SELECT p.PurchaseNumber, FullTitle, OrganizerID, party.ContactName, AuctionStartDate, p.BargainTypeID, bt.Name, bd.inn, LEFT(bd.inn,2) AS Region,kladr.NAME,  arbitrageManagerID, (am.LastName+ ' '+ am.FirstName+' '+ am.MiddleName) AS arbitrageManagerName , arbitrageTribunalNumber, p.PurchaseStatusID, ps.PurchaseStatusDesc
from Purchase p 
Inner join bankruptDetails bd  on p.purchaseID = bd.purchaseID
Inner join Party party on p.OrganizerID = party.PartyID  
Inner join ArbitrageManager am on bd.ArbitrageManagerID = am.ManagerID
Inner join BargainType bt on p.BargainTypeID = bt.BargainTypeID  
Inner join PurchaseStatus ps on p.PurchaseStatusID = ps.PurchaseStatusID
Inner join KLADR kladr on kladr.CODE = (LEFT(bd.inn,2)+'00000000000')  
where p.PurchaseStatusID in (5,7,14)