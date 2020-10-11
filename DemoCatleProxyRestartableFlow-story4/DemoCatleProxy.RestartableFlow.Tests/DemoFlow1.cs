using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;

namespace DemoCatleProxy.RestartableFlow.Tests
{
    public class Model1 
    {
        public string ReceivedMessage { get; set; }
        public string Signature { get; set; }

        public bool IsSubmitted { get; set; }
        public bool IsLoaded { get; set; }
    }

    public interface IDemoDataService
    {
        string LoadReceivedMessage();
        bool IsMessageApproved(string message);
        string GetSignature(string message);
        bool Submit(string message, string signature);
    }

    public class DemoFlow1 : Flow<Model1>
    {
        private readonly IDemoDataService _dataService;

        public DemoFlow1()
        {

        }

        public DemoFlow1(IDemoDataService dataService)
        {
            _dataService = dataService;
        }

        public override void Execute()
        {
            LoadData();

            CheckIfApproved();

            AddDigitalSignature();

            SubmitData();
        }

        public virtual void LoadData()
        {
            if (Model.IsLoaded)
            {
                throw new FlowFatalTerminateException();
            }

            Model.ReceivedMessage = _dataService.LoadReceivedMessage();
            Model.IsLoaded = true;
        }

        public virtual void CheckIfApproved()
        {
            if (!_dataService.IsMessageApproved(Model.ReceivedMessage))
            {
                throw new FlowStopException();
            }
        }

        public virtual void AddDigitalSignature()
        {
            Model.Signature = _dataService.GetSignature(Model.ReceivedMessage);
        }

        public virtual void SubmitData()
        {
            if (!_dataService.Submit(Model.ReceivedMessage, Model.Signature))
            {
                throw new FlowStopException();
            }
        }
    }

    
}
