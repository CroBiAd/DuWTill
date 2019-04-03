<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebTilling.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <h2><%: Title %>.</h2>--%>
    <h3>Background:</h3>
    <p>Seeds of a durum wheat accession (ACPFG accession number TA-00285-7) were mutagenized with 0.7% EMS and 500 mutant plants grown through one further generation to seed. 
        In the next generation (M2) DNA from 100 of these plants and the unmutagenized control ‘parent’ was sequenced following reduction by exome capture using the 106.9 Mb 
        <a href="http://sequencing.roche.com/products/nimblegen-seqcap-target-enrichment/seqcap-ez-system/seqcap-ez-designs.html">Roche NimbleGen SeqCap</a> Wheat Exome Design.</p>
    <p>A novel durum exome capture reference was built from available durum whole genome assemblies: <a href="http://opendata.earlham.ac.uk/Triticum_turgidum/EI/v1/">Kronos EI v1</a> + Svevo and sequencing reads from the control exome were aligned to the reference producing a new control-derived (CD) reference. 
        Putative mutations were identified by comparing the CD reference with each mutant plant’s sequence. A read depth is reported for each putative mutation to indicate the comparative level of certainty for the mutation call.</p>
    <h3>How to:</h3>
    <ol>
        <li>BLAST your FASTA-formatted sequence of interest in the internal BLAST portal on the<samp>&nbsp;<a runat="server" href="~/Blast">Blast</a>&nbsp;</samp>page.</li>
        <li>The most similar sequences in the available sequenced exome will be displayed within their contigs. Scroll down and expand the<samp>&nbsp;BLAST&nbsp;Search&nbsp;Results&nbsp;</samp>panel to see the base pair resolution alignment of your sequence of interest with the reference.</li>
        <li>Scroll back up and choose your favourite hit by clicking on the button to the left marked<samp>&nbsp;Select</samp>.</li>
        <li>You will be taken to the<samp>&nbsp;<a runat="server" href="~/Search">Search</a>&nbsp;</samp>page where you will find all the putative mutations in your sequence of interest called within the sequenced plants of the population. 
            Note: if you see no results, no mutations were called in these 100 plants for your sequence of interest.</li>
        <li>Results in the<samp>&nbsp;<a runat="server" href="~/Search">Search</a>&nbsp;</samp>page can be sorted by<samp>&nbsp;#, Mutant ID, Contig, Position, Depth&nbsp;</samp>and<samp>&nbsp;Chromosome</samp>&nbsp;(Chr).</li>
        <li>Column headings have short descriptions in pop-ups when you mouse over the heading. See the table below for longer descriptions.</li>
        <li>Copy or note your favourite mutant id, contig number and mutation position (plus any other details you like). You can use these to search directly in the<samp>&nbsp;<a runat="server" href="~/Search">Search</a>&nbsp;</samp>page next time, without having to repeat the BLAST.</li>
        <li>Clicking on the<samp>&nbsp;Flanking Sequence&nbsp;</samp>link will expand the sequence fragment with at least 50 bp on either side of the putative mutation. The mutation will be shown in square brackets, e.g. a G in the control that is an A in your selected mutant plant will be shown as [G/A].</li>
        <li>Each mutation called in the entire dataset has a unique number ‘#’ that you will also see in the name of the FASTA-formatted flanking sequence, e.g. >#123.</li>
        <li>Select and copy the sequence as text to your own preferred file.</li>
    </ol>
    <h3>Tilling table headers:</h3>
    <p>The output table on the<samp>&nbsp;<a runat="server" href="~/Search">Search</a>&nbsp;</samp>page contains one row for each mutation found. </p>
    <table class="table table-bordered table-condensed table-striped ">
        <thead>
            <tr>
                <th>Column name</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td><samp>#</samp></td>
                <td>Each mutation called in the entire dataset has a unique mutation number ‘#’, a.k.a. internal ID, linking together the individual mutant plant with a mutation position on a CD contig.</td>
            </tr>
            <tr>
                <td><samp>Mutant&nbsp;ID</samp></td>
                <td>Identifier of the individual plant harbouring a mutation of interest and is a number between 1-2000.</td>
            </tr>
            <tr>
                <td><samp>Contig</samp></td>
                <td>Name of contig using internal CD reference nomenclature.</td>
            </tr>
            <tr>
                <td><samp>Position</samp></td>
                <td>Mutation position in bases relative to the start of the respective CD contig.</td>
            </tr>
            <tr>
                <td><samp>Mutation&nbsp;type</samp></td>
                <td>The induced mutation type – normally either a substitution of one base for another, or a deletion of one or more bases.</td>
            </tr>
            <tr>
                <td><samp>Parent</samp></td>
                <td>Base call at the position in the non-mutagenized control 'parent'.</td>
            </tr>
            <tr>
                <td><samp>Mutant</samp></td>
                <td>Base call at the position in the EMS-mutagenized individual.</td>
            </tr>
            <tr>
                <td><samp>Depth</samp></td>
                <td>Number of reads at the mutation position (read number of each putative mutated base following removal of poor quality/duplicated sequences - mutant allele coverage).</td>
            </tr>
            <tr>
                <td><samp>Zygosity</samp></td>
                <td>Predicted zygosity of the mutation in the M2 sequenced mutant plant.</td>
            </tr>
            <tr>
                <td><samp>Chr</samp></td>
                <td>Chromosome of the sequence containing the mutation.</td>
            </tr>
            <tr>
                <td><samp>Flanking&nbsp;Sequence</samp></td>
                <td>Sequence fragment of at least 50 bp on either side of the mutation and up to 200 bp.</td>
            </tr>
        </tbody>
    </table>
</asp:Content>
