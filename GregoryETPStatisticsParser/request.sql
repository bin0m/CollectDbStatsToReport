SELECT TOP 100 p.PurchaseNumber, FullTitle, OrganizerID, party.ContactName, AuctionStartDate, p.BargainTypeID, bt.Name, bd.inn, arbitrageManagerID, arbitrageTribunalNumber, p.PurchaseStatusID, ps.PurchaseStatusDesc
from Purchase p 
Inner join bankruptDetails bd  on p.purchaseID = bd.purchaseID
--join Party party2 on bd.ArbitrageManagerID = party2.PartyID  
Inner join Party party on p.OrganizerID = party.PartyID  
Inner join BargainType bt on p.BargainTypeID = bt.BargainTypeID  
Inner join PurchaseStatus ps on p.PurchaseStatusID = ps.PurchaseStatusID
where p.PurchaseStatusID in (5,7,14)