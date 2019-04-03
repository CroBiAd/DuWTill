# DuWTill data and configuration 
## Blast portal
Local BLAST+ is freely available from National Center for Biotechnology Information (NCBI). Download the self-extracting archive `ncbi-blast-2.#.#+-win64.exe` for your desired version from ftp://ftp.ncbi.nlm.nih.gov/blast/executables/blast+. We tested the database on versions 2.6.0 and 2.7.1. Detailed instruction on installation can be found [here](https://www.ncbi.nlm.nih.gov/books/NBK52637/). Namely,
>The BLAST+ archive ... contains a built-in installer. Accepting the license agreement after double-clicking, the installer will prompt for an installation directory. ... Clicking the "Install" button, the installer will create this directory with a "doc" subdirectory containing a link to the BLAST+ user manual, an "uninstaller" for future removal of the installation, and a "bin" subdirectory where the BLAST programs and accessory utilities are kept.

Setting environment variables is not compulsory.

**Note:** this install must be outside of IIS; we recommend neutral `C:\blast-2.7.1+` (in our case).
## DECaR
1. In addition to the above create two folders `db` and `temp` in the BLAST directory (`C:\blast-2.7.1+`).
2. Download DECaR reference from https://adelaide.figshare.com/... and unzip it into `C:\blast-2.7.1\db\DECaR.fasta`
3. From a command prompt execute:
```bash
cd C:\blast-2.7.1+
bin\makeblastdb -in db\DECaR.fasta -dbtype nucl -out db\DECaR -title "Durum Exome Capture Reference (DECaR)"
```
4. In Solution Explorer open Web.config in DuWTill project. Correct `appSettings` values (located near the top of a file), if necessary, to match your installation:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  ...
  <appSettings>
    <add key="BLASTbase" value="C:\blast-2.7.1+\" />
    <add key="RefName" value="DECaR" />
    <add key="DefaultSQLiteDB" value="~/App_Data/Tilling.sqlite" />
    ...
```
## Mutations
SQLite database of called mutations Tilling.sqlite is supplied with DuWTill project in App_Data folder. There is no need to change third key in appSettings (see above) unless you are substituting your own data. In that case please refer to README in YouDB project.