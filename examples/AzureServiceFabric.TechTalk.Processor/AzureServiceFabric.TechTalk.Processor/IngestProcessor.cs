using AzureServiceFabric.TechTalk.Processor.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceFabric.TechTalk.Processor
{
    public class IngestProcessor
    {
        #region Variables

        private readonly ICloudStorage cloudStorage; 

        #endregion

        #region Constructor

        public IngestProcessor(ICloudStorage cloudStorage)
        {
            this.cloudStorage = cloudStorage;
        }

        #endregion

        #region Methods



        #endregion
    }
}
