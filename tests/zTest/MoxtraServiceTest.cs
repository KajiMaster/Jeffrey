using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RallyNow.Service;

namespace zTest
{
    [TestFixture]
    public class MoxtraServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            new MoxtraService("https://apisandbox.moxtra.com/v1/");
        }
    }
}
