--Please review

SELECT TeamID, 
       TeamName, 
       MinimumAge, 
       MaximumAge, 
       Competitive
FROM tblTeam
WHERE(Coach = @coachId);