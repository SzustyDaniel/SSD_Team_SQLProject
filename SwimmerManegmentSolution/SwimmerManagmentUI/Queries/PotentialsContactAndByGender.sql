--Please review.

SELECT s.SwimmerID, 
       MIN(s.FirstName) AS FirstName, 
       MIN(s.LastName) AS LastName, 
       MIN(DATEDIFF(YEAR, s.BirthDate, GETDATE())) AS Age, 
       COUNT(*) AS NumberOfTrainings
FROM tblPotentialSwimmer AS ps
     INNER JOIN tblSwimmer AS s ON ps.PotentialSwimmerID = s.SwimmerID
     INNER JOIN tblSwimmerTraining AS st ON ps.PotentialSwimmerID = st.SwimmerID
WHERE(ps.DateOfFirstContact >= DATEADD(MONTH, @monthsToAdd, GETDATE()))
GROUP BY s.SwimmerID
UNION
SELECT s.SwimmerID, 
       MIN(s.FirstName) AS FirstName, 
       MIN(s.LastName) AS LastName, 
       MIN(DATEDIFF(YEAR, s.BirthDate, GETDATE())) AS Age, 
       COUNT(*) AS NumberOfTrainings
FROM tblRegularSwimmer AS rs
     INNER JOIN tblSwimmer AS s ON rs.SwimmerID = s.SwimmerID
     INNER JOIN tblSwimmerTraining AS st ON rs.SwimmerID = st.SwimmerID
WHERE(rs.Team IS NOT NULL)
     AND (LOWER(s.Gender) = LOWER(@gender))
GROUP BY s.SwimmerID;