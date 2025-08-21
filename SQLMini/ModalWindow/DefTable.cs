using SQLMini.Klasy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class DefTable : Form
    {
        Query _selectedRow;
        public DefTable(Query selectedRow)
        {
            InitializeComponent();
            dgKolumny.SetStyle();
            dgPowiazania.SetStyle();
            dgProcedury.SetStyle( );

            string query = @" SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE  TABLE_NAME ='"+selectedRow.Name+ "'            ORDER BY ORDINAL_POSITION";
            DataTable dtKolumny = classData.WypelnijDane(query, selectedRow.POL);
            dgKolumny.DataSource = dtKolumny;


            query = @" SELECT o.name AS ObjectName, o.type_desc AS ObjectType
                FROM sys.sql_modules m
                JOIN sys.objects o ON m.object_id = o.object_id
                WHERE m.definition LIKE '%"+selectedRow.Name+"%'";
            DataTable dtproce = classData.WypelnijDane(query, selectedRow.POL);
            dgProcedury.DataSource = dtproce;


            query = @"SELECT 
                    fk.name AS ForeignKey,
                    tp.name AS ParentTable,
                    c1.name AS ParentColumn,
                    ref.name AS ReferencedTable,
                    c2.name AS ReferencedColumn
                FROM sys.foreign_keys fk
                JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
                JOIN sys.tables tp ON fkc.parent_object_id = tp.object_id
                JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id AND fkc.parent_column_id = c1.column_id
                JOIN sys.tables ref ON fkc.referenced_object_id = ref.object_id
                JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id AND fkc.referenced_column_id = c2.column_id
                WHERE tp.name = '" + selectedRow.Name + "' OR ref.name ='" + selectedRow.Name +"'";
            DataTable dtpow = classData.WypelnijDane(query, selectedRow.POL);
            dgPowiazania.DataSource = dtpow;
            _selectedRow = selectedRow;
        }
    }
}
