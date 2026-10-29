using System;
using UnityEngine;

namespace TheLastRoute
{
    public enum PassengerDecision { Unchecked, Cleared, Recheck, Isolated }
    public enum AnomalyKind { None, Reflection, Mark }

    [Serializable]
    public sealed class PassengerRecord
    {
        public string id;
        public string displayName;
        public string destination;
        public AnomalyKind anomaly;
        public PassengerDecision decision;
        public bool uvChecked;
        public bool mirrorChecked;

        public string Evidence =>
            (uvChecked ? (anomaly == AnomalyKind.Mark ? "UV: dấu tam giác kép — trùng mẫu cấm.\n" : "UV: không có dấu cấm.\n") : "UV: chưa kiểm tra.\n") +
            (mirrorChecked ? (anomaly == AnomalyKind.Reflection ? "Gương: phản chiếu không quay theo.\n" : "Gương: chuyển động đồng bộ.\n") : "Gương: chưa kiểm tra.\n");

        public static string DecisionLabel(PassengerDecision value)
        {
            switch (value)
            {
                case PassengerDecision.Cleared: return "Đã thông qua";
                case PassengerDecision.Recheck: return "Cần tái kiểm tra";
                case PassengerDecision.Isolated: return "Đánh dấu cách ly";
                default: return "Chưa kiểm tra";
            }
        }
    }

}
