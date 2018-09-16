using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace MoviesRentalSystems
{
    public partial class Form1 : Form
    {
        public SqlConnection conn = null;
       public  bool con,showData;

        public Form1()
        {
            InitializeComponent();
           
            string connect = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            try {
                con = false;
                conn = new SqlConnection(connect);
                conn.Open();
                con = true;
                SqlCommand cmd;
                showData = false;
                String sqlMov = "Select MovieID,Title,Genre,Rental_Cost,Year From Movies";
                String sqlRen = "Select RMID, C.FirstName as FirstName, C.LastName as LastName, C.Address as Address,M.Title as Title, M.Rental_Cost as Rental_Cost, R.DateRented, R.DateReturned From RentedMovies R join Customer C on R.CustIDFK=C.CustID join Movies M on R.MovieIDFK=M.MovieID";
                String sqlCus = "Select CustID,FirstName,LastName, Address, Phone From Customer";
                cmd = new SqlCommand(sqlMov,conn);
                SqlDataAdapter adpMov = new SqlDataAdapter(cmd);

                DataTable dtGrd = new DataTable();
                adpMov.Fill(dtGrd);
                moviesGrid.DataSource = dtGrd;

                cmd = new SqlCommand(sqlRen, conn);
                SqlDataAdapter adpRen = new SqlDataAdapter(cmd);
                DataTable dtGrd1 = new DataTable();
                adpRen.Fill(dtGrd1);
                RentalGrid.DataSource = dtGrd1;


                cmd = new SqlCommand(sqlCus, conn);
                SqlDataAdapter adpCus = new SqlDataAdapter(cmd);
                DataTable dtGrd2 = new DataTable();
                adpCus.Fill(dtGrd2);
                CustGrid.DataSource = dtGrd2;
                // dataReader = cmd.ExecuteReader();

                showData = true;



            }
            catch(Exception e)
            {
                if (con == false)
                    conn = null;
                showData = false;
                MessageBox.Show("Error");
            }
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddressCust_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cust_ID_TextChanged(object sender, EventArgs e)
        {
            
        }
        String frstName = "", lstName = "",Title="";
        String issDate = "", RetnDate = "";
        DateTime iDate,rDate;
        private void RentalGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String mobl = "";
            frstName = (string)RentalGrid[1, e.RowIndex].Value;
            lstName = (string)RentalGrid[2, e.RowIndex].Value;
            Title = (string)RentalGrid[4, e.RowIndex].Value;
            String phne = "Select Phone From Customer Where FirstName='"+frstName+"' and LastName='"+lstName+"'";
            SqlCommand cmd = new SqlCommand(phne, conn);
            SqlDataReader dataReader= cmd.ExecuteReader();
            if(dataReader.Read())
            {
                mobl = dataReader.GetString(0);
            }
            dataReader.Close();
            Cust_ID.Text = RentalGrid[0, e.RowIndex].Value.ToString();
            FirstNameCust.Text = frstName;
            LastName_Cust.Text = lstName;
            AddressCust.Text = (string)RentalGrid[3, e.RowIndex].Value;
            PhoneCust.Text = mobl;
            issDate = RentalGrid[6, e.RowIndex].Value.ToString();
            iDate = Convert.ToDateTime(issDate);
            RetnDate = RentalGrid[7, e.RowIndex].Value.ToString();
            rDate = Convert.ToDateTime(RetnDate);
            IssueMovieDate.Text = issDate;
            ReturnedDate.Text = RetnDate;


        }

        private void radioButton_Allrented_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1_movisRenatl.SelectedTab = RenatlTAb;
            String sqlRen = "Select RMID, C.FirstName as FirstName, C.LastName as LastName, C.Address as Address,M.Title as Title, M.Rental_Cost as Rental_Cost, R.DateRented, R.DateReturned From RentedMovies R join Customer C on R.CustIDFK=C.CustID join Movies M on R.MovieIDFK=M.MovieID";
          
            SqlCommand cmd = new SqlCommand(sqlRen, conn);
            SqlDataAdapter adpRen = new SqlDataAdapter(cmd);
            DataTable dtGrd1 = new DataTable();
            adpRen.Fill(dtGrd1);
            RentalGrid.DataSource = dtGrd1;
        }

        String CstId = "";
        private void CustGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CstId = CustGrid[0, e.RowIndex].Value.ToString();
            Cust_ID.Text = CstId;
             FirstNameCust.Text= (string)CustGrid[1, e.RowIndex].Value;
            LastName_Cust.Text = (string)CustGrid[2, e.RowIndex].Value;
            AddressCust.Text = (string)CustGrid[3, e.RowIndex].Value;
            PhoneCust.Text = (string)CustGrid[4, e.RowIndex].Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String delQuery = "Delete From Customer where CustID ="+CstId+"";
            SqlCommand cmd = new SqlCommand(delQuery, conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            MessageBox.Show("Customer deleted sucessfully");
            dataReader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1_movisRenatl.SelectedTab = customersTab;
            String sqlCus = "Select * From Customer";
            SqlCommand cmd = new SqlCommand(sqlCus, conn);
            SqlDataAdapter adpRen = new SqlDataAdapter(cmd);
            DataTable dtGrd1 = new DataTable();
            adpRen.Fill(dtGrd1);
            CustGrid.DataSource = dtGrd1;
        }

        private void button_Deletemovie_Click(object sender, EventArgs e)
        {
            String delQuery = "Delete From Movies where MovieID =" + MovId + "";
            SqlCommand cmd = new SqlCommand(delQuery, conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            MessageBox.Show("Movie Deleted sucessfully");
            dataReader.Close();
        }

        private void button_updatemovie_Click(object sender, EventArgs e)
        {
            tabControl1_movisRenatl.SelectedTab = MoviesTab;
            String sqlCus = "Select * From Movies";
            SqlCommand cmd = new SqlCommand(sqlCus, conn);
            SqlDataAdapter adpRen = new SqlDataAdapter(cmd);
            DataTable dtGrd1 = new DataTable();
            adpRen.Fill(dtGrd1);
            moviesGrid.DataSource = dtGrd1;
        }

        private void button_AddCustomer_Click(object sender, EventArgs e)
        {
           
            String fstName = FirstNameCust.Text;
            String lstName = LastName_Cust.Text;
            String Addrss = AddressCust.Text;
            String phne = PhoneCust.Text;

            String insQry = "Insert into Customer ([FirstName], [LastName], [Address], [Phone]) Values('" + fstName+"','"+lstName+"','"+Addrss+"','"+phne+"')";
            SqlCommand cmd = new SqlCommand(insQry, conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            MessageBox.Show("Customer Added sucessfully");
            dataReader.Close();
        }

        private void button_Addmovie_Click(object sender, EventArgs e)
        {
            String Titlemov = TitleMovie.Text;
            String genre = GenreMovie.Text;
            String Rentalmov = RentalCostMovie.Text;
            String RelDate = ReleaseDate.Text;

            String insQry = "Insert into Movies ( [Title], [Genre], [Rental_Cost], [Year]) Values('" + Titlemov + "','" + genre + "','" + Rentalmov + "','" + RelDate + "')";
            SqlCommand cmd = new SqlCommand(insQry, conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            MessageBox.Show("Movie Added sucessfully");
            dataReader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String insQuery = "Insert into RentedMovies ( [MovieIDFK], [CustIDFK], [DateRented], [DateReturned]) Values("+MovId+","+CstId+",'"+ iDate + "','"+rDate+"')";
            
            SqlCommand cmd = new SqlCommand(insQuery, conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Close();
            String sqlRen = "Select RMID, C.FirstName as FirstName, C.LastName as LastName, C.Address as Address,M.Title as Title, M.Rental_Cost as Rental_Cost, R.DateRented, R.DateReturned From RentedMovies R join Customer C on R.CustIDFK=C.CustID join Movies M on R.MovieIDFK=M.MovieID";
            MessageBox.Show("Movie issued sucessfully");
            cmd = new SqlCommand(sqlRen, conn);
            SqlDataAdapter adpRen = new SqlDataAdapter(cmd);
            DataTable dtGrd1 = new DataTable();
            adpRen.Fill(dtGrd1);
            RentalGrid.DataSource = dtGrd1;
          
            
        }
        String MovId = "";
        private void moviesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MovId= moviesGrid[0, e.RowIndex].Value.ToString();
            MovieID.Text = MovId;
            TitleMovie.Text = (string)moviesGrid[1, e.RowIndex].Value;
            GenreMovie.Text = moviesGrid[2, e.RowIndex].Value.ToString();
            RentalCostMovie.Text = moviesGrid[3, e.RowIndex].Value.ToString();
            ReleaseDate.Text = moviesGrid[4, e.RowIndex].Value.ToString();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String delQuery = "Delete From RentedMovies where CustIDFK =(Select CustID from Customer where FirstName ='"+frstName+"') and MovieIDFK =(Select MovieID from Movies where Title = '"+Title+"')";
            SqlCommand cmd = new SqlCommand(delQuery, conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            MessageBox.Show("Customer Rented Information deleted sucessfully");
            String sqlRen = "Select RMID, C.FirstName as FirstName, C.LastName as LastName, C.Address as Address,M.Title as Title, M.Rental_Cost as Rental_Cost, R.DateRented, R.DateReturned From RentedMovies R join Customer C on R.CustIDFK=C.CustID join Movies M on R.MovieIDFK=M.MovieID";
            dataReader.Close();
            cmd = new SqlCommand(sqlRen, conn);
            SqlDataAdapter adpRen = new SqlDataAdapter(cmd);
            DataTable dtGrd1 = new DataTable();
            adpRen.Fill(dtGrd1);
            RentalGrid.DataSource = dtGrd1;

        }
    }
}
