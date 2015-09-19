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
        private MoxtraService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new MoxtraService("https://apisandbox.moxtra.com/");
        }

        [Test]
        public void Should_DoStuff()
        {
            Console.Out.WriteLine(DateTime.Now.Ticks);
            _service.Login();
        }
    }
}
