using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luatsqa.coreauto
{
    public class HtmlReport
    {
        public List<string> lines = new List<string>();
        public string filename;
        public string testname;
        public DateTime starttime;
        public DateTime endtime;



        public HtmlReport(string name, string test)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            filename = path + "\\" + name + "_" + DateTime.Now.Ticks.ToString() + ".html";
            testname = test;
            starttime = DateTime.Now;
        }



        public void AddStepResult(int result, string stepdescription, string log, string screenshot = null, string errormessage = null)
        {
            string row = " <tr class=\"event-row\">";
            string id = DateTime.Now.Ticks.ToString();
            switch (result)
            {
                case 1:
                    row = row + "<td><i class=\"fa fa-check text-pass\"></i></td>";
                    break;
                case 2:
                    row = row + "<td><i class=\"fa fa-times text-fail\"></i></td>";
                    break;
                case 3:
                    row = row + "<td><i class=\"fa fa-window-close text-error\"></i></td>";
                    break;
                default:
                    row = row + "<td></td>";
                    break;
            }
            row = row + "<td>" + DateTime.Now.ToString("HH:mm:ss") + "</td><td>" + stepdescription;
            
            
            if (screenshot != null && screenshot.Length > 0)
            {
                row = row + "<a href='data:image/png;base64, " + screenshot + "' data-featherlight='image'><span class='label grey badge white-text text-white'>base64-img</span></a>";
            }
            row = row + "<span class=\"grey badge\" onclick=\"myFunction('" + id + "')\">Log Detail</span></td></tr><tr style=\"display: none;\" id=\"" + id + "\"><td colspan=\"3\">" + log;
            if (errormessage != null)
            {
                row = row + "<div class='error'>Error Message: " + errormessage + "</div></td>";
            }
            row = row + "</tr>";
            lines.Add(row);
        }



        public string generate()
        {
            endtime = DateTime.Now;
            string steps = string.Join("", lines);
            try
            {
                string content = @"<!DOCTYPE html>
<html>
<head>
<meta charset = \utf-8\ >

<meta name = \viewport\ content = \width=device-width, initial-scale=1, shrink-to-fit=no\ >

<title>Automation Test HTML Report</title>

<link rel = \apple-touch-icon\ href = \https://cdn.jsdelivr.net/gh/extent-framework/extent-github-cdn@3da944e596e81f10ac6c1042c792af8a5a741023/commons/img/logo.png\ >

<link rel =\shortcut icon\ href = \https://cdn.jsdelivr.net/gh/extent-framework/extent-github-cdn@3da944e596e81f10ac6c1042c792af8a5a741023/commons/img/logo.png\ >

<link href = \https://cdn.jsdelivr.net/gh/extent-framework/extent-github-cdn@8539670db814bf19a0ddbe9a6d19058aea0cf981/spark/css/spark-style.css\ rel = \stylesheet\ />

<link href = \https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css\ rel = \stylesheet\ />

<style>
.error{
			color: #D8000C;
			background-color: #FFBABA;
		}
</style>

</head>
<script>
function myFunction(id) {
var x = document.getElementById(id);
if (x.style.display === \none\)
{
x.style.removeProperty('display');
}
else
{
x.style.display = \none\;
}
}
</script>
<script src = \https://cdn.rawgit.com/extent-framework/extent-github-cdn/7cc78ce/spark/js/jsontree.js\ ></script>
<body class=\spa\>
<div class=\container test-list-wrapper scrollable\>
<div class=\p-v-10 d-inline-block\>
<div class=\info\>
<h5 class=\test-status text-pass\>$TESTNAME$</h5>
<span class=\badge badge-success\>$STARTTIME$</span>
<span class=\badge badge-danger\>$ENDTIME$</span>
<span class=\badge badge-default\>$DURATION$</span>
</div>
</div>
</div>
<div class=\container detail-body mt-4\>

<table class=\table table-sm\>
<thead><tr>
<th class=\status-col\>Status</th>
<th class=\timestamp-col\>Timestamp</th>
<th class=\details-col\>Details</th>
</tr></thead>
<tbody>
$STEPS$
</tbody>
</table>



</div>
<script src = \https://cdn.jsdelivr.net/gh/extent-framework/extent-github-cdn@cc89904c1a12065be1d08fc4994a258ef01b12bd/spark/js/scripts.js\ ></script>
<script type=\text/javascript\>

</script>
<div style=\position: absolute; bottom: 5px; right: 5px; margin: 0; padding: 5px 3px; \>Develop by luat.do@cir2.com</div>
</body>



</html>";
                content = content.Replace('\\', '"');



                content = content.Replace("$TESTNAME$", testname + " -- Execute Date: " + DateTime.Now.ToString("yyyy-MMM-dd"));
                content = content.Replace("$STARTTIME$", starttime.ToString("HH:mm:ss"));
                content = content.Replace("$ENDTIME$", endtime.ToString("HH:mm:ss"));
                var duration = (int)endtime.Subtract(starttime).TotalSeconds;
                content = content.Replace("$DURATION$", duration.ToString() + " sec(s)");
                content = content.Replace("$STEPS$", steps);
                //FileStream stream = new FileStream(filename, FileMode.CreateNew);
                //using (StreamWriter writer = new StreamWriter(stream))
                //{
                // writer.Write(content);
                //}
                StreamWriter writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return filename;
        }



    }
}
