using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BusinessLogic.Objects
{
    public class GuidService
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public class SingletonGuidService : GuidService
        {
        }

        public class ScopedGuidService : GuidService
        {
        }

        public class TransientGuidService : GuidService
        {
        }
    }
}
