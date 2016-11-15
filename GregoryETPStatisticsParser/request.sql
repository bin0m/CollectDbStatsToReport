select LotID, p.PurchaseNumber, LotNumber, lot.Title, PersistedInitalContractPriceValue, party.ContactName, p.FullTitle, p.AuctionStartDate, bt.Name AS BargainTypeName, bd.inn, LEFT(bd.inn,2) AS Region,kladr.NAME AS RegionName,  (am.LastName+ ' '+ am.FirstName+' '+ am.MiddleName) AS arbitrageManagerName , arbitrageTribunalNumber, ps.PurchaseStatusDesc 
from purchaseLot lot
Inner join PurchaseStatus ps on lot.PurchaseStatusID = ps.PurchaseStatusID
Inner join BargainType bt on lot.BargainTypeID = bt.BargainTypeID  
Inner join Purchase p  on lot.purchaseID = p.purchaseID
Inner join BankruptDetails bd  on p.purchaseID = bd.purchaseID
Inner join Party party on p.OrganizerID = party.PartyID  
Inner join ArbitrageManager am on bd.ArbitrageManagerID = am.ManagerID
Inner join KLADR kladr on kladr.CODE = (LEFT(bd.inn,2)+'00000000000')  
where lot.PurchaseStatusID in (5,7,14)
order by p.AuctionStartDate DESC