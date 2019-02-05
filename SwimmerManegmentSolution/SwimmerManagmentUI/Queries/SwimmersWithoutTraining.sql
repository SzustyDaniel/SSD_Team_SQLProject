SELECT rs.SwimmerID, 
       s.FirstName, 
       s.LastName, 
       s.Gender, 
       rs.Team, 
       rs.TutorID
FROM tblRegularSwimmer AS rs
     LEFT OUTER JOIN tblSwimmerTraining AS st ON rs.SwimmerID = st.SwimmerID
     INNER JOIN tblSwimmer AS s ON rs.SwimmerID = s.SwimmerID
WHERE(st.SwimmerID IS NULL);