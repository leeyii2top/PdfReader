using PdfXenon.Standard;
using System;
using System.Text;
using System.IO;
using Xunit;

namespace UnitTesting
{
    public class ASCIIReaderTests : HelperMethods
    {
        [Fact]
        public void Single1()
        {
            MemoryStream ms = StringToStream("");
            ASCIIReader r = new ASCIIReader(ms);

            Assert.True(r.Position == 0);
            Assert.True(r.ReadLine() == null);
            Assert.True(r.Position == 0);
        }

        [Fact]
        public void Single2()
        {
            MemoryStream ms = StringToStream("123");
            ASCIIReader r = new ASCIIReader(ms);

            Assert.True(r.Position == 0);
            Assert.True(r.ReadLine() == "123");
            Assert.True(r.Position == 3);
        }

        [Fact]
        public void TwoCarriageReturn()
        {
            MemoryStream ms = StringToStream("123\r456");
            ASCIIReader r = new ASCIIReader(ms);

            Assert.True(r.Position == 0);
            Assert.True(r.ReadLine() == "123");
            Assert.True(r.Position == 4);
            Assert.True(r.ReadLine() == "456");
            Assert.True(r.Position == 7);
        }

        [Fact]
        public void TwoLineFeed()
        {
            MemoryStream ms = StringToStream("123\n456");
            ASCIIReader r = new ASCIIReader(ms);

            Assert.True(r.Position == 0);
            Assert.True(r.ReadLine() == "123");
            Assert.True(r.Position == 4);
            Assert.True(r.ReadLine() == "456");
            Assert.True(r.Position == 7);
        }

        [Fact]
        public void TwoBoth()
        {
            MemoryStream ms = StringToStream("123\r\n456");
            ASCIIReader r = new ASCIIReader(ms);

            Assert.True(r.Position == 0);
            Assert.True(r.ReadLine() == "123");
            Assert.True(r.Position == 5);
            Assert.True(r.ReadLine() == "456");
            Assert.True(r.Position == 8);
        }

        [Fact]
        public void ThreeCarriageReturn()
        {
            MemoryStream ms = StringToStream("123\r\r456");
            ASCIIReader r = new ASCIIReader(ms);

            Assert.True(r.Position == 0);
            Assert.True(r.ReadLine() == "123");
            Assert.True(r.Position == 4);
            Assert.True(r.ReadLine() == "");
            Assert.True(r.Position == 5);
            Assert.True(r.ReadLine() == "456");
            Assert.True(r.Position == 8);
        }

        [Fact]
        public void ThreeLineFeed()
        {
            MemoryStream ms = StringToStream("123\n\n456");
            ASCIIReader r = new ASCIIReader(ms);

            Assert.True(r.Position == 0);
            Assert.True(r.ReadLine() == "123");
            Assert.True(r.Position == 4);
            Assert.True(r.ReadLine() == "");
            Assert.True(r.Position == 5);
            Assert.True(r.ReadLine() == "456");
            Assert.True(r.Position == 8);
        }
    }
}
