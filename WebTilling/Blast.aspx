<%@ Page Title="Blast" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Blast.aspx.cs" Inherits="WebTilling.Blast" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 form-group">
            <label for="txtBlast">Enter your nucleotide sequence in <asp:HyperLink ID="hiperFASTA" runat="server" NavigateUrl="http://tigrblast.tigr.org/euk-blast/OSA1/html/fasta.html" 
                Target="_blank">FASTA format</asp:HyperLink> &nbsp;in the text box below:</label>
            <asp:TextBox ID="txtBlast" runat="server" style="height: 120px;" TextMode="MultiLine" CssClass="form-control" OnPreRender="txtBlast_PreRender1" />
        </div>
    </div>
<%--        <div class="row">
        <div class="col-md-6 form-group">
            <label for="dropProgram">Program:</label>
            <asp:DropDownList ID="dropProgram" runat="server" CssClass="form-control">
                <asp:ListItem Selected="True" Value="0">blastn</asp:ListItem>
                <asp:ListItem Value="1">tblastn</asp:ListItem>
                <asp:ListItem Value="2">tblastx</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-6 form-group">
            <label for="dropExpect">Expect Threshold:</label>
            <asp:DropDownList ID="dropExpect" runat="server" CssClass="form-control">
                <asp:ListItem Value="0.0001">0.0001</asp:ListItem>
                <asp:ListItem Value="0.01">0.01</asp:ListItem>
                <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 form-group">
            <label for="dropAlign">Max Number of Alignments:</label>
            <asp:DropDownList ID="dropAlign" runat="server" CssClass="form-control">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Selected="True">10</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-6 form-group">
            <label for="dropDescr">Max Number of Descriptions:</label>
            <asp:DropDownList ID="dropDescr" runat="server" CssClass="form-control">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Selected="True">10</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-md-2">
<%--	        <asp:Button CssClass="btn btn-primary" ID="butBLAST" runat="server" Text="Submit" OnClick="butBLAST_Click" />--%>
            <asp:LinkButton ID="butBLAST" runat="server" CssClass="btn btn-primary" OnClick="butBLAST_Click">
                <span aria-hidden="true" class="glyphicon glyphicon-upload" /> Submit
            </asp:LinkButton>        
        </div>
        <div class="col-md-10">
            <p class="text-danger" style="overflow: hidden; text-overflow: ellipsis"><asp:Literal runat="server" ID="ErrorMessage" /></p>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">&nbsp;</div>
    </div>
    <div class="row">
        <div class="col-md-12"><%--ImageLocation="~/ZedGraphImages/ImageD.png"--%> 
            <asp:Chart ID="chartBlast" runat="server" Height="1px" IsMapEnabled="False" ImageLocation="~/Content/ImageD.png" EnableViewState="True" ImageStorageMode="UseImageLocation" SuppressExceptions="True" 
                ViewStateContent="All" ViewStateMode="Enabled" Width="1170px" BorderlineColor="204, 204, 204" CssClass="img-responsive" BorderlineDashStyle="Solid">
                <Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="RangeBar" Color="SteelBlue" CustomProperties="PixelPointWidth=15" IsVisibleInLegend="False" LabelForeColor="White" Name="Series1" YValuesPerPoint="2">
                    </asp:Series>
                </Series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" AlignmentOrientation="Horizontal">
						<AxisY IsMarginVisible="True"
							labelautofitstyle="IncreaseFont, DecreaseFont, LabelsAngleStep90" textorientation="Horizontal" titlealignment="Center" Interval="10" IntervalType="Number" LineWidth="2">
							<MajorGrid Enabled="False" Interval="Auto" />
							<MajorTickMark Interval="10" IntervalType="Number" Size="3" TickMarkStyle="AcrossAxis" />
						    <MinorTickMark Enabled="True" Interval="1" />
						    <LabelStyle IsEndLabelVisible="False" />
						</AxisY>
						<AxisX IsReversed="True" Enabled="False" Interval="1" IntervalType="Number" IsLabelAutoFit="False" />
                    </asp:ChartArea>
                </chartareas>
                <Titles>
                    <asp:Title Alignment="BottomLeft" DockedToChartArea="ChartArea1" Name="blastTitle" />
                </Titles>
            </asp:Chart>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">&nbsp;</div>
    </div>
    <asp:GridView ID="gridBLAST" runat="server" AutoGenerateSelectButton="True" CssClass="table table-bordered table-condensed table-hover" AutoGenerateColumns="False" DataKeyNames="seqID" OnSelectedIndexChanged="gridBLAST_SelectedIndexChanged">
		<HeaderStyle Wrap="False" />
		<Columns>
			<asp:BoundField DataField="seqID" HeaderText="seqID" Visible="False" ReadOnly="True" />
			<asp:BoundField DataField="Contig" HeaderText="HSP on Contigs" />
			<asp:BoundField DataField="Score" HeaderText="Bit-score" />
			<asp:BoundField DataField="Evalue" HeaderText="E-value" />
			<asp:BoundField DataField="Align" HeaderText="Align-length" />
			<asp:BoundField DataField="HitStart" HeaderText="Hit-from" />
			<asp:BoundField DataField="HitEnd" HeaderText="Hit-to" />
			<asp:BoundField DataField="Frame" HeaderText="Frame" />
		</Columns>
	</asp:GridView>
    <div class="row">
        <div class="col-sm-12">&nbsp;</div>
    </div>
    <div class="panel-group">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" href="#collapse1">BLAST Search Results</a>
                </h4>
            </div>
            <div id="collapse1" class="panel-collapse collapse">
<%--                <div class="panel-body">--%>
	                <asp:TextBox ID="txtRes" runat="server" TextMode="MultiLine" CssClass="form-control" style="height: 1200px;" Font-Names="Lucida Console,Courier New,Consolas,Monaco,Courier" Font-Size="Smaller" />
<%--                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
