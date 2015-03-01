using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System.IO;


namespace consoleNew
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> zz = new List<double>();
            xmlSerialiser serelize = new xmlSerialiser();
            var myData = serelize.deserilizer("d:/temp/new2.xml");
            foreach (var w in myData)
            {
                string z = w.Z;  
                double zValue = Convert.ToDouble(z);
                Math.Round(zValue, 4);       
                zz.Add(zValue);
             

            }

            MyClass myFun = new MyClass();
            //return matrixes 
        List<Matrix<double>> matrixesReturn=myFun.TestMethod1();

        Matrix<double> returnedChi2 = matrixesReturn[0];
        Matrix<double> msurused = matrixesReturn[1];

          var indexI = 0;
          var indexJ = 0;
          var indexI1 = 0;
          var indexJ1 = 0;
          var myE = returnedChi2.Enumerate();
          var blah=  myE.AsQueryable();
          var minFinal = blah.Min();
          var maxFinal = blah.Max();
          var myIndexed = returnedChi2.EnumerateIndexed();
 
            /*
             * find index possition for Min and Max 
             */
          foreach (var i in myIndexed)
          {
             var p=i.Item3;
             if (minFinal == p)
             {
                 indexI = i.Item1;
                 indexJ = i.Item2;
             }
             if (maxFinal == p)
             {
                 indexI1 = i.Item1;
                 indexJ1 = i.Item2;
             }
          }

          MyClass myC=new MyClass();
          List<double> oM = myC.listOM();
          List<double> oL = myC.listOL();

            //output 
          var bestchi2perdof = minFinal / zz.Count;
          var omBest = oM[indexI];
          var olBest = oL[indexJ];


            /*% Check that the mscr range we used was adequate
           * The values used must not be at the edge of those test
             * */
          var mrus = msurused.Enumerate().AsQueryable();
          var min_mscr_used = mrus.Min();
          var max_mscr_used = mrus.Max();


          Normaliz myNormalize = new Normaliz();
          List<double> mscr = myNormalize.normilizeList();

          var mscr_lo = mscr.Min();
          var mscr_hi = mscr.Max();



         
           var t= returnedChi2.EnumerateRows();
           using (StreamWriter myWriter = new StreamWriter(@"d:/temp/outputFinal2.txt"))
           {

               if (min_mscr_used <= mscr_lo) {
                   
                   myWriter.WriteLine("WARNING: Min mscr reached, extend range{0}",min_mscr_used); 
               }
               if (max_mscr_used >= mscr_hi)
               {

                   myWriter.WriteLine("WARNING: Max mscr reached, extend range {0}",max_mscr_used);
               }

               myWriter.WriteLine("Mscr range tested:{0} < mscr < {1}",mscr_lo,mscr_hi);
               myWriter.WriteLine("Mscr range used :{0} < mscr < {1}",min_mscr_used,max_mscr_used);


               

               myWriter.WriteLine("min in chi2 is {0} at index X {1} index Y {2}", minFinal, indexI, indexJ);
               myWriter.WriteLine("OMbest= {0}  , OLbest =  {1}    ,   bestchi2perdof = {2}", omBest, olBest, bestchi2perdof);


               foreach (var i in t)
               {

                   myWriter.Write(i);
               }
           }
          
        }
    }
}
