using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;


public partial class _Default : System.Web.UI.Page
{
    public const string strconneciton = ("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\welcome\\Documents\\Visual Studio 2010\\WebSites\\WebSite2\\App_Data\\encrypt.mdf;Integrated Security=True;User Instance=True");
    SqlConnection con = new SqlConnection(strconneciton);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindencryptedData();
            BindDecryptedData();
        }
    }
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        string strpassword = Encryptdata(Txtpassword.Text);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into tblpassword(UserName,Password) values('" + Txtusername.Text + "','" + strpassword + "')", con);
        cmd.ExecuteNonQuery();
        BindencryptedData();
        BindDecryptedData();
        con.Close();
    }
    protected void BindencryptedData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from tblpassword", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        con.Close();

    }
    protected void BindDecryptedData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from tblpassword", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView2.DataSource = ds;
        GridView2.DataBind();
        con.Close();
    }
    private string Encryptdata(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }
    private string Decryptdata(string encryptpwd)
    {
        string decryptpwd = string.Empty;
        UTF8Encoding encodepwd = new UTF8Encoding();
        Decoder Decode = encodepwd.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
        int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        decryptpwd = new String(decoded_char);
        return decryptpwd;
    }
protected void  GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string decryptpassword = e.Row.Cells[1].Text;
            e.Row.Cells[1].Text = Decryptdata(decryptpassword);
        }
}
protected void Button1_Click(object sender, EventArgs e)
{
   DataSet1 ds=new DataSet1;



}
}