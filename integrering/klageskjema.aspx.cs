using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class klageskjema : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSendInnKlage_Click(object sender, EventArgs e)
    {
        koblingMotAzureWFC.Service1Client client = new koblingMotAzureWFC.Service1Client();

        try
        {
            client.doStuff(txtTittel.Text, txtTittel.Text, txaBeskrivelse.Text,Convert.ToInt16(ddlTilfredsstillelse.SelectedValue), Convert.ToInt16(ddlSakstype.SelectedValue));
            litSuccess.Text = "<div class='alert alert-success'>Henvendelsen er blitt innsendt, vi vil behandle saken så fort som mulig.</div>";
        }
        catch (Exception)
        {

            
        }
        
    }
}