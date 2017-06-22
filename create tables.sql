CREATE TABLE `glb_pais` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
);

CREATE TABLE `glb_uf` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Id_GLB_Pais` int(11) NOT NULL,
  `Nome` varchar(250) NOT NULL,
  `Sigla` char(3) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_GLB_UF_GLB_Pais` (`Id_GLB_Pais`),
  CONSTRAINT `FK_GLB_UF_GLB_Pais` FOREIGN KEY (`Id_GLB_Pais`) REFERENCES `glb_pais` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE `glb_cidade` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Id_GLB_UF` int(11) NOT NULL,
  `Nome` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_GLB_Cidade_GLB_UF` (`Id_GLB_UF`),
  CONSTRAINT `FK_GLB_Cidade_GLB_UF` FOREIGN KEY (`Id_GLB_UF`) REFERENCES `glb_uf` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
);