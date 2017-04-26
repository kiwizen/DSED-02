USE [Movies]
SELECT *
FROM dbo.Movies LEFT JOIN (
  SELECT MovieIDFK, COUNT(MovieIDFK) AS RENTED
  FROM dbo.Movies
  LEFT JOIN dbo.RentedMovies
  ON MovieID=MovieIDFK 
  WHERE NOT MovieIDFK IS NULL
  AND DateReturned IS NULL
  GROUP BY MovieIDFK
) VIEW01
ON MovieID = MovieIDFK 
WHERE RENTED IS NULL Or Copies > RENTED

