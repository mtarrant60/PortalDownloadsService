declare @baseurl varchar(1000) = 'https://bbins365.sharepoint.com/:f:/r/sites/565-bbabsence/Information%20Technology/Development/TestDocDownloads'

select top 100 l.id, c.name, l.name, @baseurl + '/'+c.[name]+'/'+l.id+'/'+l.[name]+'/'
from legalcase__c l
join CustomerContract__c c on l.CustomerContractID__c = c.id
where c.name like 'unum%'
