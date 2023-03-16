-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Mar 17, 2023 at 12:11 AM
-- Server version: 5.5.25
-- PHP Version: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `apitestdatabase`
--
CREATE DATABASE `apitestdatabase` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `apitestdatabase`;

-- --------------------------------------------------------

--
-- Table structure for table `cars`
--

CREATE TABLE IF NOT EXISTS `cars` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `trade_mark_id` int(11) NOT NULL,
  `model` varchar(255) DEFAULT NULL,
  `bodytype` int(11) NOT NULL,
  `fueltype` int(11) NOT NULL,
  `tankcapacity` decimal(6,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `trade_mark_id` (`trade_mark_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `cars`
--

INSERT INTO `cars` (`id`, `trade_mark_id`, `model`, `bodytype`, `fueltype`, `tankcapacity`) VALUES
(1, 4, 'test', 1, 1, '1.44');

-- --------------------------------------------------------

--
-- Table structure for table `trade_marks`
--

CREATE TABLE IF NOT EXISTS `trade_marks` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `trade_marks`
--

INSERT INTO `trade_marks` (`id`, `name`) VALUES
(2, 'Zalupen'),
(3, NULL),
(4, NULL),
(5, 'zalupa');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cars`
--
ALTER TABLE `cars`
  ADD CONSTRAINT `cars_ibfk_1` FOREIGN KEY (`trade_mark_id`) REFERENCES `trade_marks` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
