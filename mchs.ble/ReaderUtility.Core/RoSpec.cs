using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderUtility.Core
{
    using System.Diagnostics.CodeAnalysis;

    using Init.Tools;

    using Org.LLRP.LTK.LLRPV1;
    using Org.LLRP.LTK.LLRPV1.DataType;

    public class RoSpec
    {
        /// <summary>
        /// Таймаут приема тегов
        /// </summary>
        private const int READ_TIMEOUT_SECONDS = 4;

        /// <summary>
        /// Reader
        /// </summary>
        public readonly LLRPClient reader = new LLRPClient();

        /// <summary>
        /// Удалить RoSpec
        /// </summary>
        public void DeleteRoSpec()
        {
            var msg = new MSG_DELETE_ROSPEC();
            msg.ROSpecID = 0;
            MSG_ERROR_MESSAGE msgErr;
            MSG_DELETE_ROSPEC_RESPONSE rsp = this.reader.DELETE_ROSPEC(msg, out msgErr, 2000);
            LogResult(rsp, msgErr);
        }

        /// <summary>
        /// Добавить RoSpec
        /// </summary>
        public void AddRoSpec()
        {
            MSG_ERROR_MESSAGE msgErr;
            var msg = new MSG_ADD_ROSPEC();

            // Reader Operation Spec (ROSpec)
            msg.ROSpec = new PARAM_ROSpec();

            // ROSpec must be disabled by default
            msg.ROSpec.CurrentState = ENUM_ROSpecState.Disabled;

            // The ROSpec ID can be set to any number
            // You must use the same ID when enabling this ROSpec
            msg.ROSpec.ROSpecID = 123;

            // ROBoundarySpec
            // Specifies the start and stop triggers for the ROSpec
            msg.ROSpec.ROBoundarySpec = new PARAM_ROBoundarySpec();

            // Immediate start trigger
            // The reader will start reading tags as soon as the ROSpec is enabled
            msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger = new PARAM_ROSpecStartTrigger();
            msg.ROSpec.ROBoundarySpec.ROSpecStartTrigger.ROSpecStartTriggerType = ENUM_ROSpecStartTriggerType.Immediate;

            // No stop trigger. Keep reading tags until the ROSpec is disabled.
            msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger = new PARAM_ROSpecStopTrigger();
            msg.ROSpec.ROBoundarySpec.ROSpecStopTrigger.ROSpecStopTriggerType = ENUM_ROSpecStopTriggerType.Null;

            // Antenna Inventory Spec (AISpec)
            // Specifies which antennas and protocol to use
            msg.ROSpec.SpecParameter = new UNION_SpecParameter();
            var aiSpec = new PARAM_AISpec();
            aiSpec.AntennaIDs = new UInt16Array();

            // Enable all antennas
            aiSpec.AntennaIDs.Add(0);

            // No AISpec stop trigger. It stops when the ROSpec stops.
            aiSpec.AISpecStopTrigger = new PARAM_AISpecStopTrigger();
            aiSpec.AISpecStopTrigger.AISpecStopTriggerType = ENUM_AISpecStopTriggerType.Duration;
            aiSpec.AISpecStopTrigger.DurationTrigger = READ_TIMEOUT_SECONDS * 1000;
            aiSpec.InventoryParameterSpec = new PARAM_InventoryParameterSpec[1];
            aiSpec.InventoryParameterSpec[0] = new PARAM_InventoryParameterSpec();
            aiSpec.InventoryParameterSpec[0].InventoryParameterSpecID = 1234;
            aiSpec.InventoryParameterSpec[0].ProtocolID = ENUM_AirProtocols.EPCGlobalClass1Gen2;
            msg.ROSpec.SpecParameter.Add(aiSpec);

            // Report Spec
            msg.ROSpec.ROReportSpec = new PARAM_ROReportSpec();

            // Send a report for every tag read
            msg.ROSpec.ROReportSpec.ROReportTrigger = ENUM_ROReportTriggerType.Upon_N_Tags_Or_End_Of_ROSpec;
            msg.ROSpec.ROReportSpec.N = 0;
            msg.ROSpec.ROReportSpec.TagReportContentSelector = new PARAM_TagReportContentSelector();

            MSG_ADD_ROSPEC_RESPONSE rsp = this.reader.ADD_ROSPEC(msg, out msgErr, 2000);

            LogResult(rsp, msgErr);
        }

        /// <summary>
        /// Enable RoSpec
        /// </summary>
        public void EnableRoSpec()
        {
            MSG_ERROR_MESSAGE msgErr;
            var msg = new MSG_ENABLE_ROSPEC();
            msg.ROSpecID = 123;
            MSG_ENABLE_ROSPEC_RESPONSE rsp = this.reader.ENABLE_ROSPEC(msg, out msgErr, 2000);
            LogResult(rsp, msgErr);
        }

        /// <summary>
        /// Залогировать ошибки в выполнении команд ридера
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void LogResult(Message rsp, MSG_ERROR_MESSAGE errMsg)
        {
            if (errMsg != null)
            {
                Log.Add(errMsg.ToString());
                Console.WriteLine("Возникла ошибка при работе ридера");
            }
            else if (rsp == null)
            {
                Log.Add("Timeout Error in DeleteRoSpec");
                Console.WriteLine("Возникла ошибка при работе ридера");
            }
        }
    }
}
