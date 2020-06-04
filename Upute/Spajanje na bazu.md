1. Instalirati MSSQL lokalnu bazu
    Upute: https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15

2. U MSSQL Kreirati bazu naziva Eventi

3. Pokrenuti SQL skripte za kreiranje tablice ..\Distribuirane Baze Podataka\SQL\KreiranjeTablica.sql

4. Popuniti tablice sa skriptom za punjenje ..\Distribuirane Baze Podataka\SQL\PunjenjePodataka.sql

5. U web configu promijeniti connectionString  72 linija putanja ..\Distribuirane Baze Podataka\Events\Web.config
metadata=res://*/Models.Events.csdl|res://*/Models.Events.ssdl|res://*/Models.Events.msl;provider=System.Data.SqlClient;provider connection string="data source=GEMINI\sql2016btim;initial catalog=Eventi;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework"
data source={NazivTvogServera} -- ovaj dio je potrebno izmjeniti najvjerojatnije ide localdb -> (LocalDB)\\v11.0
initial catalog={NazivBaze}    -- ovo provjeri za svaki slučaj naziv tvoje baze

6. Poziv na API http://localhost:{tvojPort}/{nazivControllera}/{id} 
    Primjer: http://localhost:54297/api/Events -> dohvaća sve evente
    Primjer: http://localhost:54297/api/Events/1 -> dohvaća event sa ID-em 1

