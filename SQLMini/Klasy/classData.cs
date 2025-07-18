using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLMini.Klasy
{
    internal class classData
    {
        public static DataTable WypelnijDane(Query q)
        {
            return WypelnijDane(q.QueryText, q.POL);
        }
        public static DataTable WypelnijDane(string procedura, string pol, bool pivot=true)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(pol))
                using (SqlCommand cmd = new SqlCommand(procedura, con))
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                {
                    if(procedura.ToLower().Contains("select"))
                        cmd.CommandType = CommandType.Text;
                    else
                        cmd.CommandType = CommandType.StoredProcedure;

                    // Tu możesz dodać parametry, np.:
                    // cmd.Parameters.AddWithValue("@Id", 123);

                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    var dane = dataSet.Tables[0];

                    if (dane.Rows.Count > 1|| pivot==false)
                        return dane;

                    if (dane.Rows.Count == 1 &&pivot)
                    {
                        DataTable MyTable = new DataTable();
                        MyTable.Columns.Add("Id", typeof(int));
                        MyTable.Columns.Add("Kolumna", typeof(string));
                        MyTable.Columns.Add("Wartosc", typeof(string));

                        for (int i = 0; i < dane.Columns.Count; i++)
                        {
                            MyTable.Rows.Add(i + 1, dane.Columns[i].ColumnName, dane.Rows[0][i].ToString());
                        }

                        return MyTable;
                    }

                    return null;
                }
            }
            catch (Exception ee)
            {
                classLog.LogError(ee, "classFun.SyberiaZapytanie");
                throw ee; // Rzucenie wyjątku dalej, aby można było go obsłużyć w wywołującym kodzie

     
                return null;
            }
        }

        public static DataTable WypelnijDane2(string zapytanie, string pol, bool showerror = false)
        {



            try
            {
                using (SqlConnection con = new SqlConnection(pol))
                {


                    SqlDataAdapter dataAdapter = new SqlDataAdapter(zapytanie, con);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    var dane = dataSet.Tables[0];
                    if (dane.Rows.Count > 1)
                        return dane;
                    if (dane.Rows.Count == 1)
                    {
                        DataTable MyTable = new DataTable(); // 1
                        MyTable.Columns.Add("Id", typeof(int));
                        MyTable.Columns.Add("Kolumna", typeof(string));
                        MyTable.Columns.Add("Wartosc", typeof(string));

                        for (int i = 0; i < dane.Columns.Count; i++)
                        {
                            MyTable.Rows.Add(i + 1, dane.Columns[i].ColumnName, dane.Rows[0][i].ToString());

                        }
                        return MyTable;


                    }
                    return null;
                }
            }
            catch (Exception ee)
            {
                if (showerror)
                {
                    classMessage.ShowError(ee.Message);
                }
                classLog.LogError(ee, "classFun.SyberiaZapytanie");
                return null;

            }
        }

       
    }
}
