using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCount;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //统计字符总数
            Basic mycount = new Basic();
            string ipath = @"input.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                        //input内容:windows95 windows98 windows2000 you and me
            string opath = "output.txt";
            mycount.setOutput(opath);
            int chars = mycount.charCount(ipath);
            Assert.AreEqual(chars, 42);
        }
        [TestMethod]
        public void TestMethod2()
        {
            //统计单词总数
            Basic mycount = new Basic();
            string path = @"input.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                       //input内容:Monday Tuesday Wednesday Thursday windows95 windows98 windows2000 you and me
            string opath = "output.txt";
            mycount.setOutput(opath);

            int words = mycount.wordCount(path);
            Assert.AreEqual(words, 3);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //将文本分割为单词
            Basic mycount = new Basic();
            string path = @"input.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                       //input内容:Monday Tuesday Wednesday Thursday windows95 windows98 windows2000 you and me

            string opath = "output.txt";
            mycount.setOutput(opath);

            List<string> splitlist = mycount.splitWord(path);
            string result1 = "";
            string result2 = "windows95 windows98 windows2000 ";

            for (int i = 0; i < splitlist.Count; i++)
            {
                result1 += splitlist[i] + " ";
            }
            //统计词频
            int n = 4;
            mycount.printWord(splitlist);
            mycount.printWord(splitlist, n);
            
            Assert.AreEqual(result1, result2);
        }

        [TestMethod]
        public void TestMethod4()
        {
            //统计行数
            Basic mycount = new Basic();
            string path = @"input.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                       

            string opath = "output.txt";
            mycount.setOutput(opath);

            int line = mycount.lineCount(path);
            Assert.AreEqual(line, 1);
        }

        [TestMethod]
        public void TestMethod5()
        {
            //词组
            Extend mycount = new Extend();
            string path = @"input.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                       
            string opath = "output.txt";
            mycount.setOutput(opath);
            int m = 4;//指定词组长度为4

            mycount.printPhrase(path,  m);
            
        }

        [TestMethod]
        public void TestMethod6()
        {
            //统计单词总数
            Basic mycount = new Basic();
            string path = @"input2.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                       
            string opath = "output2.txt";
            mycount.setOutput(opath);

            int words = mycount.wordCount(path);
            Assert.AreEqual(words, 34);
        }

        [TestMethod]
        public void TestMethod7()
        {
            //将文本分割为单词
            Basic mycount = new Basic();
            string path = @"input2.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                       

            string opath = "output2.txt";
            mycount.setOutput(opath);

            List<string> splitlist = mycount.splitWord(path);
            string result1 = "";
            string result2 = "hello name newyork ";

            for (int i = 0; i < 3; i++)
            {
                result1 += splitlist[i] + " ";
            }
            //统计词频
            int n = 4;
            mycount.printWord(splitlist);
            mycount.printWord(splitlist, n);

            Assert.AreEqual(result1, result2);
        }

        [TestMethod]
        public void TestMethod8()
        {
            //统计行数
            Basic mycount = new Basic();
            string path = @"input2.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下


            string opath = "output2.txt";
            mycount.setOutput(opath);

            int line = mycount.lineCount(path);
            Assert.AreEqual(line, 9);
        }

        [TestMethod]
        public void TestMethod9()
        {
            //词组
            Extend mycount = new Extend();
            string path = @"input2.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下

            string opath = "output2.txt";
            mycount.setOutput(opath);
            int m = 4;//指定词组长度为4

            mycount.printPhrase(path, m);

        }

        [TestMethod]
        public void TestMethod10()
        {
            //统计英文版茶花女第一章的单词及词频
            Basic mycount = new Basic();
            string path = @"input3.txt";//保存在了：WordCount\UnitTestProject1\bin\Debug目录下
                                        

            string opath = "output3.txt";
            mycount.setOutput(opath);

            List<string> splitlist = mycount.splitWord(path);
            
            //统计词频
            int n = 12;
            mycount.printWord(splitlist);
            mycount.printWord(splitlist, n);

        }
    }
}
