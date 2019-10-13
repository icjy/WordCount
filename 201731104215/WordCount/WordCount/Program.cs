using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordCount
{
    class Program
    {       
        static void Main(string[] args)
        {

            //默认的文件路径
            string ipath= @"H:\WordCount\WordCount\bin\Debug\input.txt";
            //默认的输出路径
            string opath = @"H:\WordCount\WordCount\bin\Debug\output.txt";
            //StreamReader sR = new StreamReader(@"C:\Users\Administrator\Desktop\Camellias2.txt", Encoding.UTF8);
            Basic mycount = new Basic();
            Extend mycount2 = new Extend();

            int m = 1;
            int n = 10;
            
            for (int len = 0; len < args.Length; len++)
            {
                if (args[len] == "-i")//文件路径
                {
                    ipath = args[len + 1];

                }
                else if (args[len] == "-o")//存储路径
                {
                    opath = args[len + 1];
                    mycount.setOutput(opath);
                    mycount2.setOutput(opath);

                    mycount.lineCount(ipath);
                    mycount.charCount(ipath);
                    mycount.wordCount(ipath);
                    List<string> list1 = mycount.splitWord(ipath);
                    mycount.printWord(list1);
                }
                else if (args[len] == "-m")//词组长度
                {
                    m = int.Parse(args[len + 1]);
                    mycount2.printPhrase(ipath, m);
                }
                else if (args[len] == "-n")//单词数量
                {
                    n = int.Parse(args[len + 1]);
                    List<string> list2 = mycount.splitWord(ipath);
                    mycount.printWord(list2,n);

                }
                
            }
            
        }

    }

    public class Basic
    {
        private string opath;
        public string getOutput()
        {
            return opath;
        }

        public void setOutput(string opath)
        {
            this.opath = opath;
        }

        Extend mycount2 = new Extend();
        //StreamWriter consoleWriter = new StreamWriter(@"output.txt");//E:\VSProject\WordCount\WordCount\bin\Debug


        //统计行数
        public int lineCount(string path)
        {
            int line = 0;
            string text;
            StreamReader sR = new StreamReader(path, Encoding.UTF8);
            while ((text = sR.ReadLine()) != null)
            {
                line++;
            }
            sR.Close();
            Console.WriteLine("================基础功能================");
            mycount2.storeTest("================基础功能================",opath);
            Console.WriteLine("行数:" + line);
            mycount2.storeTest("行数:" + line,opath);
            return line;
        }

        //统计字符总数
        public int charCount(string path)
        {
            int chars = 0;
            string text;
            StreamReader sR = new StreamReader(path, Encoding.UTF8);
            while ((text = sR.ReadLine()) != null)
            {
                chars += Regex.Matches(text, @"\w").Count;//匹配字母，数字，_
                chars += Regex.Matches(text, @"\s").Count;//匹配空格，水平制表符
                chars += Regex.Matches(text, @"\n").Count;//匹配换行符
                chars += Regex.Matches(text, @"\?").Count;//匹配?
                chars += Regex.Matches(text, @"\,").Count;//匹配逗号
                chars += Regex.Matches(text, @"\.").Count;//匹配.
                chars += Regex.Matches(text, @"\!").Count;//匹配!
                chars += Regex.Matches(text, @"\-").Count;//匹配-
                chars += Regex.Matches(text, @"\(").Count;//匹配(
                chars += Regex.Matches(text, @"\)").Count;//匹配)
            }
            sR.Close();
            Console.WriteLine("字符总数:" + chars);
            mycount2.storeTest("字符总数:" + chars, opath);
            return chars;
        }


        //统计单词总数
        public int wordCount(string path)
        {
            int words = 0;
            string wordtype = @"[a-zA-Z]{4,}[a-zA-Z0-9]*";
            string text;
            StreamReader sR = new StreamReader(path, Encoding.UTF8);
            while ((text = sR.ReadLine()) != null)
            {
                words += Regex.Matches(text, wordtype).Count;
            }
            sR.Close();
            Console.WriteLine("单词总数:" + words);
            mycount2.storeTest("单词总数:" + words, opath);
            return words;
        }


        //将文本分割为单词
        public List<string> splitWord(string path)
        {
            List<string> list = new List<string>();
            StreamReader sR = new StreamReader(path, Encoding.UTF8);
            string text;
            while ((text = sR.ReadLine()) != null)
            {
                //将单词用split分割，分割的依据为空格
                string[] words = text.Trim().Split(new char[] { '\n', '?', ',', '.', '!', '-', '(', ')', '_',' ','\r'});// \r回车， \n换行   
                Regex regex = new Regex ( @"[a-zA-Z]{4,}[a-zA-Z0-9]*");
                foreach (string word in words)
                {
                    //统计单词出现的次数
                    if (word != "" && word != null && regex.IsMatch(word))
                    {

                        list.Add(word.ToLower());
                    }
                }
            }
            sR.Close();
            return list;
        }



        //统计最多的10个单词及其词频
        public void printWord(List<string> list)
        {
            //第一步，统计所有单词及其词频
            Dictionary<string, int> list2 = new Dictionary<string, int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list2.ContainsKey(list[i]))
                {
                    list2[list[i]]++;//相当于list2["A"]++,计算A的数量
                }
                else
                {
                    list2.Add(list[i], 1);
                }
            }


            //第二步，按词频降序
            Dictionary<string, int> dic = list2.OrderByDescending(p => p.Value).ToDictionary(o => o.Key, p => p.Value);
            //升序为OrderBy，降序为OrderByDescending

            //第三步，统计10个单词及其词频
            //Dictionary取key值，非要采用for的方法也可
            List<string> test = new List<string>(dic.Keys);

            int para;
            if (dic.Count <= 10)
            {
                para = dic.Count;
                printTenWord(dic, para, test);

            }
            else if (dic.Count > 10)
            {
                para = 10;
                printTenWord(dic, para, test);

            }
        }

        //带有参数n的统计
        public void printWord(List<string> list, int n)
        {
            //第一步，统计所有单词及其词频
            Dictionary<string, int> list2 = new Dictionary<string, int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list2.ContainsKey(list[i]))
                {
                    list2[list[i]]++;//相当于list2["A"]++,计算A的数量
                }
                else
                {
                    list2.Add(list[i], 1);
                }
            }


            //第二步，按词频降序
            Dictionary<string, int> dic = list2.OrderByDescending(p => p.Value).ToDictionary(o => o.Key, p => p.Value);
            //升序为OrderBy，降序为OrderByDescending

            //第三步，统计10个单词及其词频
            //Dictionary取key值，非要采用for的方法也可
            List<string> test = new List<string>(dic.Keys);

            if (n > dic.Count)
            {
                Console.WriteLine("参数n超出索引范围！请重新输入！");
                mycount2.storeTest("参数n超出索引范围！请重新输入！",opath);

            }
            else
            {
                Console.WriteLine("================" + n + "个单词及其词频================");
                mycount2.storeTest("================" + n + "个单词及其词频================", opath);
                printTenWord(dic, n, test);
                
            }
        }

        public void printTenWord(Dictionary<string, int> dic,int para, List<string> test)
        {
            int num = 0;
            int index = 0;
            for (int i = 0; i < para; i++)
            {
                if (i == 0)
                {
                    if (dic[test[i]] == dic[test[i + 1]])
                    {
                        num = 2;
                        index = i;
                    }
                    else
                    {
                        Console.WriteLine("单词:"+test[i].ToLower() + "   频数:" + dic[test[i]]);
                        mycount2.storeTest("单词:" + test[i].ToLower() + "   频数:"+ dic[test[i]],opath);
                        num = 0;
                    }
                }
                else if (i == para - 1)
                {
                    if (dic[test[i]] == dic[test[i - 1]])
                    {
                        Dictionary<string, int> result1 = new Dictionary<string, int>();
                        for (int j = 0; j < num; j++)
                        {
                            result1.Add(test[index + j], dic[test[index + j]]);
                        }
                        Dictionary<string, int> result2 = result1.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                        foreach (string item in result2.Keys)
                        {
                            Console.WriteLine("单词:"+item.ToLower()+ "   频数:" + dic[item]);
                            mycount2.storeTest("单词:"+item.ToLower() + "   频数:" + dic[item],opath);
                        }
                    }
                    else
                    {
                        Console.WriteLine("单词:"+test[i].ToLower() + "   频数:" + dic[test[i]]);
                        mycount2.storeTest("单词:"+test[i].ToLower() + "   频数:" + dic[test[i]], opath);
                    }
                }
                else
                {
                    if (dic[test[i]] > dic[test[i + 1]])
                    {
                        if (dic[test[i]] == dic[test[i - 1]])
                        {
                            Dictionary<string, int> result1 = new Dictionary<string, int>();
                            for (int j = 0; j < num; j++)
                            {
                                result1.Add(test[index + j], dic[test[index + j]]);
                            }
                            Dictionary<string, int> result2 = result1.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                            foreach (string item in result2.Keys)
                            {
                                Console.WriteLine("单词:"+item.ToLower() + "   频数:" + dic[item]);//"{0}:{1}", item.ToLower(), dic[item]
                                mycount2.storeTest("单词:"+item.ToLower() + "   频数:" + dic[item],opath);
                            }
                        }
                        else
                        {
                            Console.WriteLine("单词:"+test[i].ToLower() + "   频数:" + dic[test[i]]);
                            mycount2.storeTest("单词:"+test[i].ToLower() + "   频数:" + dic[test[i]], opath);
                        }
                        num = 0;
                    }
                    else if (dic[test[i]] == dic[test[i + 1]])
                    {
                        if (num == 0)
                        {
                            num = 2;
                            index = i;
                        }
                        else
                        {
                            num++;
                        }
                    }
                }
            }
        }

        //public void WriteLine(string str)
        //{

        //    consoleWriter.WriteLine(str);
        //    Console.WriteLine(str);
        //    consoleWriter.Flush();
        //}

    }

    public class Extend
    {
        private string opath;
        public string getOutput()
        {
            return opath;
        }

        public void setOutput(string opath)
        {
            this.opath = opath;
        }

        public void storeTest(string sb,string opath)
        {
            //String path = "e:/subject.txt";
            FileStream fs = new FileStream(opath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(sb);
            sw.Close();
            fs.Close();
        }

        //将文本分割为词组
        public void printPhrase(string path, int m)
        {
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            Dictionary<string, int> list3 = new Dictionary<string, int>();
            StreamReader sR = new StreamReader(path, Encoding.UTF8);
            string text;
            while ((text = sR.ReadLine()) != null)
            {
                //将单词用split分割，分割的依据为空格
                string[] words = text.Trim().Split(new char[] { '\n', '?', ',', '.', '!', '-', '(', ')', '_', ' ', '\r' });// \r回车， \n换行   

                Regex regex = new Regex(@"[a-zA-Z]{4,}[a-zA-Z0-9]*");
                foreach (string word in words)
                {
                    //统计单词出现的次数
                    if (word != null && word != "" && regex.IsMatch(word))
                    {
                        list.Add(word);
                    }
                }
            }
            if (list.Count < m)
            {
                Console.WriteLine("参数m超出索引范围！请重新输入！");
                storeTest("参数m超出索引范围！请重新输入！",opath);
                return;//跳出方法
            }

            Console.WriteLine("================长度为" + m + "的所有词组================");
            storeTest("================长度为" + m + "的所有词组================", opath);

            //两个for循环存储数组表
            for (int i = 1; i <= list.Count - m + 1; i++)
            {
                string str = list[i - 1];
                for (int ii = i; ii < i + m - 1; ii++)
                {
                    str += " " + list[ii];
                }
                list2.Add(str);
            }
            for (int i = 0; i < list2.Count; i++)
            {
                if (list3.ContainsKey(list2[i]))
                {
                    list3[list2[i]]++;//相当于list2["词组"]++,计算词组的数量
                }
                else
                {
                    list3.Add(list2[i], 1);
                }
            }

            var result = from pair in list3 orderby pair.Key select pair;
            foreach (KeyValuePair<string, int> pair in result)
            {
                Console.WriteLine("词组:" + pair.Key.ToLower() + "   频数:" + pair.Value);
                storeTest("词组:" + pair.Key.ToLower() + "   频数:" + pair.Value, opath);
            }

        }
    }
    //class Outputw
    //{
    //    static string output;
    //    public String getOutput()
    //    {
    //        return output;
    //    }

    //    public void setOutput(String output)
    //    {
    //        this.output = output;
    //    }
    //    StreamWriter consoleWriter = new StreamWriter(output);
    //    public void WriteLine(string str)
    //    {

    //        consoleWriter.WriteLine(str);
    //        Console.WriteLine(str);
    //        consoleWriter.Flush();
    //    }
    //}
}
