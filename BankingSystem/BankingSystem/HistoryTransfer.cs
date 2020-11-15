using System;

namespace Bank
{
    class HistoryTransfer
    {
        public DateTime Date { get; private set; }
        public string AccountFrom { get; private set; }
        public string AccountTo { get; private set; }
        public string NameTransfer { get; private set; }
        public float ValueTransfer { get; private set; }
        public string TypeTransfer { get; private set; }

        public HistoryTransfer(DateTime date, string accountFrom, string accountTo, string nameTransfer, float valueTransfer, string typeTransfer)
        {
            Date = date;
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            NameTransfer = nameTransfer;
            ValueTransfer = valueTransfer;
            TypeTransfer = typeTransfer;
        }
    }
}
