﻿using eBike.Data.Entities.Security;
using eBikeSystem.BLL;
using eBikeSystem.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSites_Purchasing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // are you logged on?
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                // now that you are logged on, are you in the approved role for this page?
                if (!User.IsInRole(SecurityRoles.Purchasing) && !User.IsInRole(SecurityRoles.WebsiteAdmins))
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }

            if (User.IsInRole(SecurityRoles.Staff))
            {
                var sysmgr = new UserManager();

                string employeename = sysmgr.Get_EmployeeFullName(User.Identity.Name);

                EmployeeNameLabel.Text = "Current user: " + employeename;
            }
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void GetCreatePO_Click(object sender, EventArgs e)
    {
        VendorName.Text = "Vendor Name: " + VendorDDL.SelectedValue;
        VendorCity.Text = "Vendor City: " + VendorDDL.SelectedValue;
        VendorPhone.Text = "Vendor Phone: " + VendorDDL.SelectedValue;

        VendorName.Visible = true;
        VendorCity.Visible = true;
        VendorPhone.Visible = true;
        CurrentPOLabel.Visible = true;
        CurrentInventoryLabel.Visible = true;


        CurrentPOListView.DataSourceID = "CurrentPOODS";
        CurrentPOListView.DataBind();

        CurrentInventoryListView.DataSourceID = "CurrentInventoryODS";
        CurrentInventoryListView.DataBind();

        if (CurrentPOListView.Items.Count == 0)
        {
            // DO LOGIC TO CREATE SUGGESTED PURCHASE ORDER HERE, THEN BIND CurrentPOListView AGAIN
            MessageUserControl.ShowInfo("No active purchase order found. Suggested Purchase Order Generated.");


            CurrentPOListView.DataSourceID = "SuggestedPOODS";
            CurrentPOListView.DataBind();

        }

    }

    protected void CurrentPOListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

    }

    protected void CurrentInventoryListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

    }
}