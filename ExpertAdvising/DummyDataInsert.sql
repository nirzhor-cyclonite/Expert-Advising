use ExpertAdvisingTrial

select * from Expert where expertiseSEctor = 'architecture'
select * from Expertise
select * from UserInfo

insert into Expert 
(ExpertId,FirstName,LastName,ProfileImage,Password,WalletStatus,ExpertiseSector,BankAccount,ActiveStatus) values 
('ishrak.jaman@gmail.com','Ishrak','Uz Zaman','~/Expert/ishrak.jaman@gmail.com.png','khfA0@',0,'Architecture','100200300','ACTIVE'),
('rahim_afroz@gmail.com','Md Rahim','Afroz','~/Expert/rahim_afroz@gmail.com.png','mhrzP^g',1500,'Finance','150292300','ACTIVE'),
('sabbirhassan@outlook.com','Sabbir','Hassan','~/Expert/sabbirhassan@outlook.com.png','kasdl00',0,'Finance','1204950300','ACTIVE'),
('mithila_sharmin@gmail.com','Mithila','Sharmin','~/Expert/mithila_sharmin@gmail.com.png','mnbvcx',0,'Architecture','9823007193','ACTIVE'),
('fabian123@gmail.com','Fabian','Geyrhalter','~/Expert/fabian123@gmail.com.png','Fabian123',1000,'Finance','10345997123856','ACTIVE'),
('caroline_Mc@gmail.com','Caroline','McCarthy','~/Expert/caroline_Mc@gmail.com.png','Caroline123',1200,'Architecture','90345977123896','ACTIVE'),
('bruce_law@gmail.com','Bruce','Heitz','~/Expert/bruce_law@gmail.com.png','BruceHeitzLaw',1500,'Law','20345397123844','ACTIVE'),
('developerDavo@gmail.com','Jhon','Davo','~/Expert/developerDavo@gmail.com.png','Developer_Davo',1700,'Software & Technology','41345987123851','ACTIVE'),
('terrence_Quo@gmail.com','Terrence','Quo','~/Expert/terrence_Quo@gmail.com.png','TerrenceQuo123',2000,'Software & Technology','22345017123853','ACTIVE'),
('david1996@gmail.com','David','Jhon','~/Expert/david1996@gmail.com.png','DavidMedicine',1150,'Medical','60345997123852','ACTIVE'),
('christa_bakos@gmail.com','Christa','Bakos','~/Expert/christa_bakos@gmail.com.png','ChristaHelthcare321',1300,'Medical','20345497123856','ACTIVE'),
('montina_portis@gmail.com','Montina','Portis','~/Expert/montina_portis@gmail.com.png','Montina121',1800,'Architecture','10345497123832','ACTIVE'),
('chris32Lema@gmail.com','Chris','Lema','~/Expert/chris32Lema@gmail.com.png','Chris1998',1800,'Textile','10349397123854','ACTIVE')

update Expert set ExpertiseSector='Finance' where ExpertId='caroline_Mc@gmail.com'

insert into Expertise values
('ishrak.jaman@gmail.com','B.Arch, M. Phil. In Arch (CUHK, Hong Kong)','Founder at Startups.com','Ecological Architecture, Sustainable built environment, Housing, Sustainable Open Space Planning.',220,4,'I have the pleasure of working with 100s of the best housing start-ups every year on everything from ideation. I love getting my hands dirty - and lead teams of talented consultants, writers, analysts, and marketers who love a challenge.'),
('mithila_sharmin@gmail.com','B.Arch., MUD (The university of Hong Kong)','Corporate Architect','Sustainable Architecture, Landscape Design, Accessibility.',220,5,'I can show anyone how to get better skills on landscape design using my techniques and strategies no matter how little experience you have or what type of product you are selling.'),
('fabian123@gmail.com','B.Com. (Hons), M.Com.(RU), Ph.D.(Ranchi , USA)','Brand Strategist and Creative Director at Consultancy FINIEN','Product Branding , Product Naming , Creative Strategy',150,3.5,'Renowned Brand Strategist and Creative Director. Fabian Geyrhalter is a prolific author and speaker on the subject of branding. He is the founder and principal of Los Angeles-based brand consultancy FINIEN.'),
('caroline_Mc@gmail.com','BBA (Berlin University), MBA (MSM-Netherlands), PHD (MSM-Netherlands)','TV journalist covering tech startups and social media','Business, Banking',200,4,'Journalist turned marketer.I specialize in brand storytelling, social media strategy, and event curation. Nonprofit advisor on the side'),
('bruce_Law@gmail.com','United States Naval Academy Annapolis, Maryland B.S.; Major: Political Science, Engineering Columbus School of Law','Attorney & Counselor At Law','Governmental, Commercial, Corporate and Individual Law',300,5,'Mr. Heitz served as a former Regional Counsel with the Eighth National Bank Region for the Comptroller of the Currency (OCC). Mr. Heitz is highly regarded by bank regulators and his financial institution clients. He has testified as a banking expert in jurisdictions throughout Texas.'),
('developerDavo@gmail.com','Bachelor of Computer Engineering at University Of Alberta, Master of Structural Engineering at University Of Alberta','Programmer at TED TALKS','Programming , Image Processing , Database Management System',100,4.5,'Ready to scale your programming skills and need advice? We can discuss best practices to help you scale up your technology knowledges regarding programming , database and image processing'),
('terrence_Quo@gmail.com','B.Sc. Engg. (CSEE), M.Sc(Network Systems), Ph.D. (King''s College London), PGCert HE, UK','Software developer at AutoDesk','Software Development ,Opeating System , Networks',220,4,'I have served as a software developer at over 5+ Startups in the last 4 years. If you are a non-tech entrepreneur with a strong background and a blockbuster idea , let me help you get your product to market.'),
('david1996@gmail.com','MBBS M.D. (MEDICINE)(USA), Fellow- Pediatric Ophthalmology and Strabismology','Honorary Consultant','Medicine , Paediatric',120,4,'Dr. David is an expert in Medicines with specific expertise in ophthalmology. He applies principles of ophthalmology  to determine the cause of injuries related to implants, motor vehicle crashes, falls, sports, and in the workplace.'),
('christa_Bakos@gmail.com','MBBS M.D. (MEDICINE)','Registered Nurse','Medicine',200,4,'Christa Bakos is a Registered Nurse who specializes in acute care, with a specific emphasis on wound care. Her career in healthcare includes broader experience in outpatient care, critical care, case management, and acute care nursing. Christa applies her expertise to forensic casework evaluating the quality of patient care provided in medical facilities,outpatient wound clinics.'),
('montina_Portis@gmail.com','B.Arch., M.A., Ph.D., University of North London, UK.','CEO & Agency Owner','Architectural Education, Housing ,Social Architecture.',200,4,'Experience is a very long and expensive teacher. Want to shorten the learning curve? Book time with me! I have been guiding architectural teams since 2010 .'),
('chris32Lema@gmail.com','B. Sc. in Tex. Tech., M. Sc. in Tex. Engg. (TUD, Germany), Ph.D. (Romania).','Professor','Apparel Manufacturing, Wet Processing',300,4.5,'Chris has helped dozens of small business owners break through their fears and plateaus over the years. Many right here on Expert Advising.He can help regarding textile issues .')

select * from Expertise

insert into UserInfo (UserId,FirstName,LastName,ProfileImage,Password,WalletStatus,Interest,ActiveStatus) values
('kazi.tuna.5@gmail.com','Kazi','Tuna','~/UserInfo/kazi.tuna.5@gmail.com.png','Tuna1998',500,'Medicine, Healthcare','ACTIVE'),
('tirtho.dev007@gmail.com','Devopriyo','Tirtho','~/UserInfo/tirtho.dev007@gmail.com.png','TirthoDevo',800,'Architecture, Textile','ACTIVE'),
('farzanamayisha11@gmail.com','Maisha','Farzana','~/UserInfo/farzanamayisha11@gmail.com.png','MaishaAust',600,'Architecture, Software & Technology','ACTIVE'),
('maliha@gmail.com','Maliha','Chowdhury','~/UserInfo/maliha@gmail.com.png','1xYz6',200,'Database, Cyber Security','ACTIVE'),
('nashfin@gmail.com','Noorun','Nashfin','~/UserInfo/nashfin@gmail.com.png','12ab56',320,'Machine Learning','ACTIVE'),
('sujoy@gmail.com','Sujoy','Chowdhury','~/UserInfo/sujoy@gmail.com.png','cd34t6',180,'HTML, CSS, PHP','ACTIVE'),
('nirzhor@gmail.com','Nirzhor','Chowdhury','~/UserInfo/nirzhor@gmail.com.png','qwerty',230,'Data Science, Networking','ACTIVE')

select * from UserInfo

select * from Session

insert into Session (UserId,ExpertId,CompletionStatus,Topic) values
('nirzhor@gmail.com','montina_portis@gmail.com','UPCOMING','Architecture'),
('nashfin@gmail.com','sabbirhassan@outlook.com','UPCOMING','HTML'),
('maliha@gmail.com','rahim_afroz@gmail.com','COMPLETED','Cyber Security'),
('nashfin@gmail.com','david1996@gmail.com','UPCOMING','Medicine & Healthcare'),
('maliha@gmail.com','terrence_Quo@gmail.com','COMPLETED','Software & Technology'),
('sujoy@gmail.com','fabian123@gmail.com','COMPLETED','Finance'),
('nirzhor@gmail.com','bruce_law@gmail.com','UPCOMING','Law'),
('kazi.tuna.5@gmail.com','christa_bakos@gmail.com','COMPLETED','Healthcare'),
('tirtho.dev007@gmail.com','chris32Lema@gmail.com','UPCOMING','Textile'),
('farzanamayisha11@gmail.com','montina_portis@gmail.com','COMPLETED','Architecture'),
('nashfin@gmail.com','bruce_law@gmail.com','COMPLETED','Law'),
('farzanamayisha11@gmail.com','developerDavo@gmail.com','UPCOMING','Software & Technology'),
('tirtho.dev007@gmail.com','montina_portis@gmail.com','COMPLETED','Architecture')

select * from Session where CompletionStatus='COMPLETED'

select * from Review
select UserReview from Review where SessionId in (select SessionId from Session where ExpertId='montina_portis@gmail.com')

select * from Session where ExpertId='developerDavo@gmail.com' and CompletionStatus='UPCOMING'
select count(SessionId) from Session where ExpertId='fabian123@gmail.com' and CompletionStatus='COMPLETED'
select count(SessionId) from Session where ExpertId='fabian123@gmail.com' and CompletionStatus='UPCOMING'
select UserReview from Review where SessionId in (select SessionId from Session where ExpertId='montina_portis@gmail.com')
select count(UserReview) from Review where SessionId in (select SessionId from Session where ExpertId='montina_portis@gmail.com')

select avg(Rating) from Review where SessionId in (select SessionId from Session where ExpertId='montina_portis@gmail.com' and CompletionStatus='COMPLETED')
select avg(Rating) from Review where SessionId in (select SessionId from Session where ExpertId='rahim_afroz@gmail.com' and CompletionStatus='COMPLETED')

select * from Expert where ExpertiseSector='Finance'

select * from Review
select * from Session
select * from SessionDetails

select * from Expert where ExpertId in (select ExpertId from Session where UserId='nirzhor@gmail.com' and CompletionStatus='UPCOMING')
select * from Expertise where ExpertId in (select ExpertId from Session where UserId='nirzhor@gmail.com' and CompletionStatus='UPCOMING')


select * from Session where CompletionStatus='COMPLETED'

insert into Review values
(2, 5, 'Amazing & very informative!'),
(4, 4, 'Helpful'),
(5, 5, 'Very precious insights, highly recommend.'),
(7,4,'I honestly felt that Terrence cared more to give me the most value from the call then I did. It is very rare to find someone who will care more about helping you then they care about making money. He is genuine sincere and straight to the point.'),
(9,3,'Fabian has excellent advice. Helped with extremely clear thought process. Thank You Fabian.'),
(10,4,'Christa was super helpful and provided insight that helped me make a more informed decision. Definitely will consult with her again in the future.'),
(12,5,'She was transparent, sharp, spot on.'),
(15,4,'He is right to the point. Did not waste time. I felt like i have got the advise and clarity to work on my next step. Will keep refining, trying and talking to tom again.'),
(16,5,'She was straight to the points and provided a wealth of information in the short time we have together.')

select count(ExpertId) from Expert where ExpertiseSector='Software & Technology'
select * from Expert where ExpertId='fabian123@gmail.com'
select * from Expertise where ExpertId='fabian123@gmail.com'

select * from Session where ExpertId='montina_portis@gmail.com' 

select count(SessionId) from Session where CompletionStatus='COMPLETED' and ExpertId='montina_portis@gmail.com'



