--Part 1
	DESCRIBE jobs;
	-- ID: int Primary Key Auto_Increment
	-- Name: longtext
	-- EmployerId: int
--Part 2
	SELECT * FROM employers
    WHERE employers.Location = "St. Louis City";
--Part 3
	SELECT DISTINCT skills.Name, skills.Description FROM skills
	INNER JOIN jobSkills
	ON skills.id = jobSkills.SkillId
	INNER JOIN jobs
	ON jobSkills.JobId = jobs.Id
	ORDER BY skills.Name;

