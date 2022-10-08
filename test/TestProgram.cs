namespace DirCopy.Test
{
    [TestClass]
    public class TestProgram
    {
        [TestMethod]
        public void TestMainWithoutIgnoredFolder()
        {
            var temp = Path.GetTempPath() + Guid.NewGuid() + Path.DirectorySeparatorChar;
            string srcDir = $"{temp}srcDir";
            Directory.CreateDirectory($"{srcDir}/dir0/dir1");
            Directory.CreateDirectory($"{srcDir}/dir2/dir3/dir4");
            Directory.CreateDirectory($"{srcDir}/dir5/dir6");
            Directory.CreateDirectory($"{srcDir}/dir7");
            File.WriteAllText($"{srcDir}/dir0/0.txt", "0");
            File.WriteAllText($"{srcDir}/dir0/00.txt", "00");
            File.WriteAllText($"{srcDir}/dir0/dir1/1.txt", "1");
            File.WriteAllText($"{srcDir}/dir0/dir1/11.txt", "11");
            File.WriteAllText($"{srcDir}/dir2/2.txt", "2");
            File.WriteAllText($"{srcDir}/dir2/dir3/3.txt", "3");
            File.WriteAllText($"{srcDir}/dir2/dir3/333.txt", "333");
            File.WriteAllText($"{srcDir}/dir5/dir6/4.txt", "4");
            File.WriteAllText($"{srcDir}/dir7/5.txt", "5");

            string dstDir = $"{temp}destDir";
            Program.Main(new []{"--Src", srcDir, "--Dest", dstDir });

            Assert.IsTrue(File.Exists($"{dstDir}/dir0/0.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/0.txt"), "0");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/00.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/00.txt"), "00");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/dir1/1.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/dir1/1.txt"), "1");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/dir1/11.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/dir1/11.txt"), "11");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/2.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/2.txt"), "2");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/dir3/3.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/dir3/3.txt"), "3");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/dir3/333.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/dir3/333.txt"), "333");
            
            Assert.IsTrue(File.Exists($"{dstDir}/dir5/dir6/4.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir5/dir6/4.txt"), "4");
            Assert.IsTrue(File.Exists($"{dstDir}/dir7/5.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir7/5.txt"), "5");

            Directory.Delete(temp, true);
        }

        [TestMethod]
        public void TestMainWith1IgnoredFolder()
        {
            var temp = Path.GetTempPath() + Guid.NewGuid() + Path.DirectorySeparatorChar;
            string srcDir = $"{temp}srcDir";
            Directory.CreateDirectory($"{srcDir}/dir0/dir1");
            Directory.CreateDirectory($"{srcDir}/dir2/dir3/dir4");
            Directory.CreateDirectory($"{srcDir}/dir5/dir6");
            Directory.CreateDirectory($"{srcDir}/dir7");
            File.WriteAllText($"{srcDir}/dir0/0.txt", "0");
            File.WriteAllText($"{srcDir}/dir0/00.txt", "00");
            File.WriteAllText($"{srcDir}/dir0/dir1/1.txt", "1");
            File.WriteAllText($"{srcDir}/dir0/dir1/11.txt", "11");
            File.WriteAllText($"{srcDir}/dir2/2.txt", "2");
            File.WriteAllText($"{srcDir}/dir2/dir3/3.txt", "3");
            File.WriteAllText($"{srcDir}/dir2/dir3/333.txt", "333");
            File.WriteAllText($"{srcDir}/dir5/dir6/4.txt", "4");
            File.WriteAllText($"{srcDir}/dir7/5.txt", "5");

            string dstDir = $"{temp}destDir";
            Program.Main(new []{"--Src", srcDir, "--Dest", dstDir, "--Ignore", $"{srcDir}/dir5" });

            Assert.IsTrue(File.Exists($"{dstDir}/dir0/0.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/0.txt"), "0");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/00.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/00.txt"), "00");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/dir1/1.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/dir1/1.txt"), "1");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/dir1/11.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/dir1/11.txt"), "11");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/2.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/2.txt"), "2");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/dir3/3.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/dir3/3.txt"), "3");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/dir3/333.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/dir3/333.txt"), "333");

            Assert.IsTrue(!Directory.Exists($"{dstDir}/dir5"));
            Assert.IsTrue(File.Exists($"{dstDir}/dir7/5.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir7/5.txt"), "5");
            
            Directory.Delete(temp, true);
        }

        [TestMethod]
        public void TestMainWith2IgnoredFolder()
        {
            var temp = Path.GetTempPath() + Guid.NewGuid() + Path.DirectorySeparatorChar;
            string srcDir = $"{temp}srcDir";

            Directory.CreateDirectory($"{srcDir}/dir0/dir1");
            Directory.CreateDirectory($"{srcDir}/dir2/dir3/dir4");
            Directory.CreateDirectory($"{srcDir}/dir5/dir6");
            Directory.CreateDirectory($"{srcDir}/dir7");
            File.WriteAllText($"{srcDir}/dir0/0.txt", "0");
            File.WriteAllText($"{srcDir}/dir0/00.txt", "00");
            File.WriteAllText($"{srcDir}/dir0/dir1/1.txt", "1");
            File.WriteAllText($"{srcDir}/dir0/dir1/11.txt", "11");
            File.WriteAllText($"{srcDir}/dir2/2.txt", "2");
            File.WriteAllText($"{srcDir}/dir2/dir3/3.txt", "3");
            File.WriteAllText($"{srcDir}/dir2/dir3/333.txt", "333");
            File.WriteAllText($"{srcDir}/dir5/dir6/4.txt", "4");
            File.WriteAllText($"{srcDir}/dir7/555.txt", "555");

            string dstDir = $"{temp}destDir";
            Program.Main(new []{"--Src", srcDir, "--Dest", dstDir,
                "--Ignore", $"{srcDir}/dir5", $"{srcDir}/dir7" });

            Assert.IsTrue(File.Exists($"{dstDir}/dir0/0.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/0.txt"), "0");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/00.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/00.txt"), "00");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/dir1/1.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/dir1/1.txt"), "1");
            Assert.IsTrue(File.Exists($"{dstDir}/dir0/dir1/11.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir0/dir1/11.txt"), "11");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/2.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/2.txt"), "2");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/dir3/3.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/dir3/3.txt"), "3");
            Assert.IsTrue(File.Exists($"{dstDir}/dir2/dir3/333.txt"));
            Assert.AreEqual(File.ReadAllText($"{dstDir}/dir2/dir3/333.txt"), "333");

            Assert.IsTrue(!Directory.Exists($"{dstDir}/dir5"));
            Assert.IsTrue(!Directory.Exists($"{dstDir}/dir7"));
            
            Directory.Delete(temp, true);
        }

        [TestMethod]
        public void TestNormalizeDirectoryPath()
        {
            var str = @"D:\dir0\dir1\dir2\dir3\dir4\..\..\dir5\dir6\dir7\";
            var normalizedPath = Path.GetFullPath(str).TrimEnd('/', '\\');
            Assert.AreEqual(@"D:\dir0\dir1\dir2\dir5\dir6\dir7", normalizedPath);
        }
    }
}