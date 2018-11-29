using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6
{
    public static class Program
    {
        static void Main(string[] args)
        {
            List<int> a = new List<int> {1, 2, 3};

            Dictionary<int, string> ball_number_color = new Dictionary<int, string>
            {
                {0, "红"},
                {1, "红"},
                {2, "红"},
                {3, "黄"},
                {4, "黄"},
                {5, "黄"},
                {6, "蓝"},
                {7, "蓝"},
                {8, "蓝"},
                {9, "黑"},
                {10, "白"}
            };
            List<Box> boxes = new List<Box>
            {
                new Box("箱子A", 1, 3),
                new Box("箱子B", 2, 3),
                new Box("箱子C", 3, 5)
            };


            List<List<int>> matrix = new List<List<int>>();
            int balls = ball_number_color.Count;
            for (int i = 0; i < balls; i++)
            {
                matrix.Add(a);
            }

            var allCombination = Cartesian(matrix).ToList();
            Console.WriteLine("笛卡尔积总数为:" + allCombination.Count);
            var lists = FliterList(allCombination, boxes);

            List<string> results = new List<string>();
            foreach (var list in lists)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    var intlist = list.ToList();
                    foreach (var box in boxes)
                    {
                        if (intlist[i] == box.serialNumber)
                        {
                            box.desc += ball_number_color[i];
                        }
                    }
                }

                string allDesc = "";
                foreach (var box in boxes)
                {
                    allDesc += box.desc + " ";
                    box.desc = box.name + " ";
                }

                results.Add(allDesc);
            }

            results = results.Distinct().ToList();
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(results.Count());

            Console.ReadKey();
        }

        /// <summary>
        /// 获取满足每个箱子条件的组合
        /// </summary>
        /// <param name="allCombination"></param>
        /// <param name="boxes"></param>
        /// <returns></returns>
        private static List<IEnumerable<int>> FliterList(List<IEnumerable<int>> allCombination,
            List<Box> boxes)
        {
            var result = allCombination;

            foreach (var box in boxes)
            {
                result = result.Where(t => t.Count(va => va == box.serialNumber) == box.size).ToList();
            }

            return result;
        }

        /// <summary>
        /// 求集合的笛卡尔积
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Cartesian<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> tempProduct = new[] {Enumerable.Empty<T>()};
            return sequences.Aggregate(tempProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] {item})
            );
        }
    }

    /// <summary>
    /// 箱子
    /// </summary>
    public class Box
    {
        /// <summary>
        /// 箱子名称
        /// </summary>
        public string name;

        /// <summary>
        /// 箱子编号
        /// </summary>
        public int serialNumber;

        /// <summary>
        /// 箱子编号
        /// </summary>
        public string desc;

        /// <summary>
        /// 箱子大小
        /// </summary>
        public int size;

        public Box(string name, int serialNumber, int size)
        {
            this.name = name;
            this.serialNumber = serialNumber;
            this.size = size;
            desc = name + " ";
        }
    }
}