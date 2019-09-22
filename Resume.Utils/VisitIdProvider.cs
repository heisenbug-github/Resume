using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Utils
{
    public class VisitIdProvider
    {
        public VisitIdProvider()
        {
            this.visitId = Guid.NewGuid();
        }

        private readonly Guid visitId;
        public Guid VisitId { get { return this.visitId; } }
    }
}
