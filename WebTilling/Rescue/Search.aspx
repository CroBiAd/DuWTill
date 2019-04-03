<%@ Page Title="Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebTilling.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Get all mutations for all Mutants occuring within a specific Contig.</legend>
        <div class="col-md-4 form-group">
            <label for="txtContig">Contig:</label>
            <asp:TextBox ID="txtContig" runat="server" CssClass="form-control" placeholder="i.e. ctg000060" Text="ctg000060" OnPreRender="txtContig_PreRender" CausesValidation="True" MaxLength="50" />
            <asp:RegularExpressionValidator ID="regexCont" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtContig" ValidationExpression="[\w* ]*"></asp:RegularExpressionValidator>
        </div>
        <div class="col-md-4 form-group">
            <label for="txtStart">Position Start:</label>
            <asp:TextBox ID="txtStart" runat="server" CssClass="form-control" placeholder="Number" OnPreRender="txtStart_PreRender" CausesValidation="True" MaxLength="10" />
            <asp:RangeValidator ID="rangeStart" runat="server" ErrorMessage="*" Type="Integer" MinimumValue="-1" MaximumValue="1000000000" Visible="True" ControlToValidate="txtStart" ForeColor="Red"></asp:RangeValidator>
        </div>
        <div class="col-md-4 form-group">
            <label for="txtEnd">Position End:</label>
            <asp:TextBox ID="txtEnd" runat="server" CssClass="form-control" placeholder="Number" OnPreRender="txtEnd_PreRender" CausesValidation="True" MaxLength="10" />
            <asp:RangeValidator ID="rangeEnd" runat="server" ErrorMessage="*" Type="Integer" MinimumValue="-1" MaximumValue="1000000000" Visible="True" ControlToValidate="txtEnd" ForeColor="Red"></asp:RangeValidator>
        </div>
    </fieldset>
    <fieldset>
        <legend>Alternativly, get all mutations on all Contigs for a specific Mutant.</legend>
        <div class="col-md-4 form-group">
            <label for="txtMut">Mutant ID:</label>
            <asp:TextBox ID="txtMut" runat="server" CssClass="form-control " placeholder="i.e. TA00285-7_mutant249_M2 or 249" OnPreRender="txtMut_PreRender" CausesValidation="True" MaxLength="50" />
            <asp:RegularExpressionValidator ID="regexMut" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMut" ValidationExpression="[\w* -]*"></asp:RegularExpressionValidator>
<%--            <asp:RangeValidator ID="rangeMut" runat="server" ErrorMessage="*" Type="Integer" MinimumValue="-1" MaximumValue="1000000" Visible="True" ControlToValidate="txtMut" ForeColor="Red"></asp:RangeValidator>--%>
        </div>
    </fieldset>
    <div class="row">
        <div class="col-md-12">
            <asp:LinkButton ID="butSearch" runat="server" CssClass="btn btn-primary" OnClick="butSearch_Click">
                <span aria-hidden="true" class="glyphicon glyphicon-search" /> Search
            </asp:LinkButton>        
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">&nbsp;</div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:Label ID="labRecords" runat="server"></asp:Label>
            <asp:GridView ID="gridMutations" runat="server" AutoGenerateColumns="False" DataKeyNames="mutID" DataSourceID="SQLiteDataMutations" CssClass="table table-bordered table-condensed table-hover" 
                OnDataBound="gridMutations_DataBound" AllowSorting="True" OnRowDataBound="gridMutations_RowDataBound" Caption="<span class='help-block text-right'>For How-to and Column Headers description see <a href='About'>About</a> page.</span>" RowHeaderColumn="mutID" CaptionAlign="Bottom">
                <Columns>
                    <asp:BoundField DataField="mutID" HeaderText="#" SortExpression="mutID" ReadOnly="True" />
                    <asp:BoundField DataField="mutant" HeaderText="Mutant ID" SortExpression="mutant" ReadOnly="True" />
                    <asp:BoundField DataField="ctg" HeaderText="DECaR Contig" SortExpression="ctg" ReadOnly="True" />
                    <asp:BoundField DataField="position" HeaderText="Position" SortExpression="position" ReadOnly="True" />
                    <asp:BoundField DataField="mutType" HeaderText="Mutation type" ReadOnly="True" />
                    <asp:BoundField DataField="parentAllele" HeaderText="Parent" ReadOnly="True" ItemStyle-CssClass="text-center" >
                        <ItemStyle CssClass="text-center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="mutAllele" HeaderText="Mutant" ReadOnly="True" ItemStyle-CssClass="text-center" >
                        <ItemStyle CssClass="text-center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="depth" HeaderText="Depth" SortExpression="depth" ReadOnly="True" />
                    <asp:BoundField DataField="zygosity" HeaderText="Zygosity" ReadOnly="True" />
                    <asp:BoundField DataField="chr" HeaderText="Chr" SortExpression="chr" ReadOnly="True" ItemStyle-CssClass="text-center" >
                        <ItemStyle CssClass="text-center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Flanking Sequence">
                        <ItemTemplate>
                            <div style="width: 150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                <a href="javascript:void(0)" data-toggle="collapse" data-target='#div<%# Eval("mutID") %>' class="accordion-toggle" ><%# Eval("flankSeq") %></a>
                            </div>                    
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="collapse" HeaderStyle-CssClass="collapse" FooterStyle-CssClass="collapse">
                        <ItemTemplate>
                            <tr>
                               <td colspan="11" class="active" style="padding:0">
                                 <div id='div<%# Eval("mutID") %>' class="collapse out">
                                    <asp:TextBox ID="txtSeq" runat="server" CssClass="form-control" Width="100%" TextMode="MultiLine" Height="120px" />
                                 </div>
                              </td>
                            </tr>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="SQLiteDataMutations" runat="server" ProviderName="System.Data.SQLite.EF6">
        <SelectParameters>
			<asp:ControlParameter ControlID="txtContig" DefaultValue="" Name="cont" PropertyName="Text" Type="String" ConvertEmptyStringToNull="False" />
		    <asp:ControlParameter ControlID="txtStart" DefaultValue="-1" Name="posstart" PropertyName="Text" Type="Int64" />
            <asp:ControlParameter ControlID="txtEnd" DefaultValue="-1" Name="posend" PropertyName="Text" Type="Int64" />
		    <asp:ControlParameter ControlID="txtMut" DefaultValue="" Name="mut" PropertyName="Text" Type="String" ConvertEmptyStringToNull="False" />
		</SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

