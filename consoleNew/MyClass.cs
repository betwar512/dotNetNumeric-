﻿using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace consoleNew
{
    class MyClass
    {


            public double omI { get; set; }
            public double olI { set; get; }

            public List<double> om { get; set; }
            public List<double> ol { get; set; }

            public double selectedZ { get; set; }
           
            public List<Matrix<double>> TestMethod1()
            {
                List<Matrix<double>> myMatrixes = new List<Matrix<double>>();

                //dmList init here to have keep data 
                List<double> myList = new List<double>();

                xmlSerialiser serelize = new xmlSerialiser();
                Normaliz normal = new Normaliz();

                List<double>  myZZ = new List<double>();
                List<double> muModel = new List<double>();
                //muerr list + 0.02
                List<double> muerrList = new List<double>();
                IEnumerable<double> result = null;
                List<double> muList = new List<double>();
                List<double> mscr = normal.normilizeList();



                var myData = serelize.deserilizer("d:/temp/new2.xml");
                foreach (var w in myData)
                {
                    string z = w.Z;
                    string muerr = w.MUERR;
                    string mu = w.MU;
                    double muerrValue = Convert.ToDouble(muerr);
                    muerrValue += 0.02;
                    muerrValue = Math.Round(muerrValue,4);

                    double muValue = Convert.ToDouble(mu);
                   
                   
                    Math.Round(muerrValue, 4);
                    double zValue = Convert.ToDouble(z);
                    Math.Round(zValue, 4);
                    muerrList.Add(muerrValue);
                    myZZ.Add(zValue);
                    muList.Add(muValue);

                }
                om = listOM();
                ol = listOL();
                //matrix
                Matrix<double> chi2 = Matrix<double>.Build.Dense(om.Count, ol.Count, Math.Exp(20));
                var mscrUsed = Matrix<double>.Build.Dense(myZZ.Count, myZZ.Count);
                var listFinal = new List<double>();
                for (int i = 0; i < om.Count; i++)
                {
                    for (int j = 0; j < ol.Count; j++)
                    {
                        omI = om[i];
                        olI = ol[j];
                        //mu_MOdel
                        muModel.Clear();

                        muModel = DistMod(myZZ, omI, olI);

                        //Mu_model normal 
                        for (int k = 0; k < mscr.Count; k++)
                        {
                            double round = Math.Round(mscr[k], 4);

                            //replaced
                            //muModel.ForEach(delegate(double mm)
                            //{

                            //    double muModelNormal = mm + compare;
                            //});

                            for (int u = 0; u < muModel.Count; u++)
                            {
                                muModel[u] += round;
                            }
                                //replaced
                                //muerrList.ForEach(delegate(double g) { double f = Math.Pow(g, 2); });

                                for (int q = 0; q < muerrList.Count; q++)
                                {
                                    muerrList[q] = Math.Pow(muerrList[q], 2);
                                    muerrList[q] = Math.Round(muerrList[q], 4);
                                }
                                //result of top syntax 
                                IEnumerable<double> topElemet = muModel.Zip(muList, (x, y) => Math.Pow((x - y), 2));
                                foreach (double o in topElemet)
                                {
                                    var t = Math.Round(o);

                                }

                                //result bottom syntax
                                result = topElemet.Zip(muerrList, (x, y) => x / y);

                                double chi2_test = Math.Round(result.Sum());
                                Console.WriteLine("chi2_test is = {0}", chi2_test);
                                listFinal.Add(chi2_test);

                                double testCompare = Math.Round(chi2[i, j], 4);
                                if (!(double.IsNaN(chi2_test)) && (chi2_test < testCompare))
                                {
                                    chi2[i, j] = chi2_test;
                                    mscrUsed[i, j] = round;
                                }
                            
                            //caculate chi2 to put it into the list 
                        }
                    }
                }
                myMatrixes.Add(chi2);
                myMatrixes.Add(mscrUsed);

                return myMatrixes;
               
            }
           

            //DisMode
            public List<double> DistMod(List<double> zz, double om, double ol)
            {

                double ok = 1.0 - om - ol;
                double R0;

                List<double> x = new List<double>();
                //list D
                List<double> DM = new List<double>();
                double X = 0;

                for (int i = 0; i < zz.Count; i++)
                {


                    //delegate pointer for function f
                    Func<double, double> myFunction = f;
                    //pointer
                    selectedZ = zz[i];
                    X = MathNet.Numerics.Integration.SimpsonRule.IntegrateThreePoint(myFunction, 0, selectedZ);
                    var roundedX = Math.Round(X, 4);
                    x.Add(roundedX);
                };

                if (ok < 0.0)
                {
                    var negOk = ok * (-1);
                    R0 = 1 / Math.Sqrt(negOk);
                    x.ForEach(delegate(double t)
                    {
                        double y = R0 * Math.Sin(t / R0);
                    });

                }
                else
                    if (ok > 0.0)
                    {
                        R0 = 1 / Math.Sqrt(ok);
                        x.ForEach(delegate(double t)
                        {
                            double y = R0 * Math.Sinh(t / R0);
                        });
                        //  D = R0 * Math.Sinh(X / R0);
                    }
                //else
                //{

                //   D = X;
                //}

                for (int i = 0; i < zz.Count; i++)
                {
                    double ran = x[i] * (1 + zz[i]);

                    double y = 5 * Math.Log10(ran);
                    DM.Add(y);
                }


                //    double lumDist = D * (1 + selectedZ);
                //   double DM = 5 * Math.Log10(lumDist);
                //   dm.Add(DM);
                return DM;
            }
            //Function f
            public double f(double z)
            {
                var x = omI;
                var y = olI;
                z = selectedZ;
                double t = HzInverserse(z, x, y);
                return t;
            }

            /*
             * this is the cosmological model we are testing, written in terms of z 
             *(redshift) instead of scale factor a
             */
            public double HzInverserse(double z, double om, double ol)
            {

                double Hz = Math.Sqrt(Math.Pow((1+z), 2) * (om * (z+1)) - (ol * z * (z+2)));
                double hZi = 1.0 / Hz;
                return hZi;

            }
            //Create OM Array 
            public List<double> listOM()
            {

                List<double> OM = new List<double>();
                for (double i = 0.0; i < 0.7; )
                {
                    i += 0.005;
                    var e = Math.Round(i, 4);
                    OM.Add(e);
                }
                return OM;
            }
            //Create OL Array 
            public List<double> listOL()
            {
                List<double> OL = new List<double>();
                for (double i = 0.0; i < 1; )
                {
                    i += 0.005;
                    var e = Math.Round(i, 4);
                    OL.Add(e);
                }
                return OL;
            }

        }
     
    }


