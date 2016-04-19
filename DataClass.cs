using System; 
2 using System.Collections.Generic; 
3 using System.Linq; 
4 using System.Web; 
5 //libraries need to talk to database 
6 using System.Data; 
7 using System.Data.SqlClient; 
8 using System.Configuration; 
9 
 
10 /// <summary> 
11 /// This class will connect to the database 
12 /// It will have methods to retrieve the services 
13 /// It will also retrieve all the grants for that service 
14 /// John Voorhess 4/12/2016 
15 /// </summary> 
16 public class DataClass 
17 { 
18     SqlConnection connect; 
19     public DataClass() 
20     { 
21         connect = new SqlConnection(ConfigurationManager.ConnectionStrings["CommunityAssistConnectionString"].ToString()); 
22     }//end constructor 
23 
 
24     public DataTable GetAuthors() 
25     { 
26         DataTable tbl = null; 
27 
 
28         string sql = "SELECT authorkey, authorname FROM Author"; 
29         SqlCommand cmd = new SqlCommand(sql, connect); 
30      
31         tbl = ReadData(cmd); 
32      
33         return tbl; 
34     } 
35 
 
36     public DataTable GetBooks(int AuthorKey) 
37     { 
38         DataTable tbl = null; 
39         string sql = "SELECT * FROM book INNER JOIN authorbook ON book.bookkey= authorbook.bookkey WHERE authorkey = @authorkey"; 
40 
 
41         SqlCommand cmd = new SqlCommand(sql, connect); 
42         cmd.Parameters.AddWithValue("@AuthorKey", AuthorKey); 
43 
 
44         tbl = ReadData(cmd); 
45 
 
46         return tbl; 
47     } 
48 
 
49     private DataTable ReadData(SqlCommand cmd) 
50     { 
51         SqlDataReader reader = null; 
52         DataTable tbl = new DataTable(); 
53 
 
54 
 
55         connect.Open(); 
56         reader = cmd.ExecuteReader(); 
57         tbl.Load(reader); 
58         reader.Close(); 
59         connect.Close(); 
60 
 
61 
 
62         return tbl; 
63     } 
64 
 
65 }//end class 
