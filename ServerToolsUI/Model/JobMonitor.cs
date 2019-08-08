using ServerToolsIdrac.Redfish.Actions;
using ServerToolsIdrac.Redfish.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ServerToolsUI.Model
{
    public class JobMonitor : INotifyPropertyChanged
    {
        public JobMonitor(NetworkCredential credentials, int refreshTime)
        {
            this.credentials = credentials;
            Jobs = new ObservableCollection<JobsDataGridInfo>();
            timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, refreshTime)
            };
            timer.Tick += Timer_Tick;
        }

        private readonly DispatcherTimer timer;
        
        private readonly NetworkCredential credentials;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<JobsDataGridInfo> jobs;
        public ObservableCollection<JobsDataGridInfo> Jobs
        {
            get => jobs;
            set
            {
                if(value != jobs)
                {
                    jobs = value;
                    NotifyPropertyChanged("Jobs");
                }
            }
        }

        private bool isRunning = false;
        public bool IsRunning
        {
            get => isRunning;
            set
            {
                if(value != isRunning)
                {
                    isRunning = value;
                    NotifyPropertyChanged("IsRunning");
                }
            }
        }

        public void AddJob(string server, string uri)
        {
            Jobs.Add(new JobsDataGridInfo()
            {
                SerialNumber = "Unknow",
                JobId = "Unknow",
                JobName = "Unknow",
                JobMessage = "Unknow",
                JobPercentComplete = 0,
                JobStatus = "Unknown",
                Server = server,
                JobUri = uri
            });
        }

        public void RemoveJob(string server)
        {
            JobsDataGridInfo jobToRemove = Jobs.Where(x => x.Server.Equals(server)).FirstOrDefault();
            Jobs.Remove(jobToRemove);
        }

        public void Start()
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                IsRunning = true;
            }

        }

        public void Stop()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                IsRunning = false;
            }
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            await UpdateJobsAsync();
            timer.Start();
        }

        private async Task UpdateJobsAsync()
        {
            try
            {
                foreach (var job in Jobs)
                {
                    try
                    {
                        if (job.JobStatus.Contains("Failed") || job.JobStatus.Contains("Completed"))
                            continue;

                        if (string.IsNullOrEmpty(job.SerialNumber))
                        {
                            ChassisAction chassisAction = new ChassisAction(job.Server, credentials);
                            job.SerialNumber = await chassisAction.GetServiceTagAsync();
                        }

                        if (string.IsNullOrEmpty(job.JobId))
                        {
                            TaskAction taskAction = new TaskAction(job.Server, credentials);
                            job.JobId = await taskAction.GetTaskIdAsync(job.JobUri);
                        }

                        JobAction action = new JobAction(job.Server, credentials);
                        Job idracJob = await action.GetJobAsync(job.JobId);
                        job.JobName = idracJob.Name;
                        job.JobId = idracJob.Id;
                        job.JobMessage = idracJob.Message;
                        job.JobPercentComplete = idracJob.PercentComplete;
                        job.JobStatus = idracJob.JobState;
                    }
                    catch(Exception ex)
                    {
                        job.JobMessage = ex.Message;
                    }
                }
            }
            catch { } // Changes made asynchronously to the collection
        }
    }
}
