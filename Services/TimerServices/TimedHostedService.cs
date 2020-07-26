using System;
using System.Threading;
using System.Threading.Tasks;

using App;

using DAL;
using DAL.Services;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Services.TimerServices
{
    public class LeasingTimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;

        public LeasingTimedHostedService(ILogger<LeasingTimedHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var infoBip = new InfoBipSendSmsService(AppSettingsParser.Settings);

            var liste = Service.GetEcheances();
            using (var _context = new ReminderContext())
            {
                foreach (var item in liste)
                {
                    var notification = item.ToSMS();

                    if (infoBip.Send(notification))
                    {
                        _context.Notifications.AddAsync(notification);
                    }
                }
                _context.SaveChangesAsync();
            }

            _logger.LogInformation("Timed Background Service is working.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}