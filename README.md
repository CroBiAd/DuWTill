# DuWTill 
DuWTill is a simple Web client to the database of mutations in the South Australian Durum Wheat Tilling (DuWTill) collection created as part of a joint project between Australian Centre for Plant Functional Genomics (ACPFG), University of Saskatchewan and Università degli Studi della Tuscia for the project “A Molecular Diversity Drive for Precision-Engineered Wheat”.

## Data content
Seeds of a durum wheat accession (ACPFG accession number TA-00285-7) were mutagenized with 0.7% EMS and 500 mutant plants grown through one further generation to seed. In the next generation (M2) DNA from 100 of these plants and the unmutagenized control ‘parent’ (TA-00285-7) was sequenced following reduction by exome capture using the 106.9 Mb [Roche NimbleGen SeqCap](https://sequencing.roche.com/en/products-solutions/by-category/target-enrichment/shareddesigns.html) Wheat Exome Design. 

A novel Durum Exome Capture Reference (DECaR) was built: reads from the unmutagenized control sample were mapped into two publically available durum wheat whole genome assemblies of [Svevo](https://www.interomics.eu/durum-wheat-genome-disclosure-agreement) and [Kronos EI v1](https://opendata.earlham.ac.uk/opendata/data/Triticum_turgidum/EI/v1.1/), and then combined with assembled unmapped control reads. The resulting reference covered 4% of total durum genomic space.

Reads of the 100 exome-captured samples were aligned to DECaR. Any variation from DECaR was considered a mutation if and only if it was present in only one mutant sample. 

For further details and to cite:
>Mario Fruzangohar, Elena Kalashyan, Priyanka Kalambettu, Jennifer Ens, Krysta Wiebe, Curtis J. Pozniak, Penny J. Tricker, Ute Baumann "Novel Informatics Tools to Support Functional Annotation of the durum wheat genome." (In preparation)

## DuWTill Web Server
The identified mutations are accessible on a public DuWTill Web server http://crobiad.agwine.adelaide.edu.au/duwtill.

## Local Build (Windows)
We included two projects in this distribution: DuWTill itself and YourDB console application, handy if you want to investigate your own data. **For further instructions, please refer a README in each project**.
### Prerequisites
1. **Internet Information Services (IIS)**. Turn this Windows feature on from the `Control Panel->Turn Windows features on or off`. There is a good instruction on how to do it [here](https://www.howtogeek.com/112455/how-to-install-iis-8-on-windows-8/). We used IIS 8.0 Express for development and IIS 10.0 on DuWTill Web server. 
2. **Visual Studio 2015 or later**, downloadable from the [official Web site](https://visualstudio.microsoft.com/vs/older-downloads/).
### Build
1. Clone or download the repository to your computer.
2. Open the `WebTilling.sln` file in Visual Studio. 
3. Follow the instruction in README in each project
4. Build the solution.

These steps should resolve dependencies and download required packages. You are ready to publish the application to IIS.
