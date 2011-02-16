SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

-- --------------------------------------------------------

--
-- Struttura della tabella `categories`
--

CREATE TABLE IF NOT EXISTS `categories` (
  `c_id` int(11) NOT NULL auto_increment,
  `c_name` varchar(255) NOT NULL,
  `c_status` tinyint(1) NOT NULL,
  PRIMARY KEY  (`c_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=20 ;

--
-- Dump dei dati per la tabella `categories`
--

INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(1, 'Veicolo abbandonato', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(2, 'Fermata dell''autobus', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(3, 'Parcheggio auto', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(4, 'Animali randagi', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(5, 'Manifesti abusivi', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(6, 'Spazzatura in strada', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(7, 'Graffiti', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(8, 'Pavimentazione stradale', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(9, 'Marciapiedi', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(10, 'Buche', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(11, 'Bagni pubblici', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(12, 'Segnali stradali', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(13, 'Strade e Autostrade', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(14, 'Pulizia strade', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(15, 'Illuminazione', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(16, 'Nomi della strada', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(17, 'Luci semaforiche', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(18, 'Alberi, giardini, spazi pubblici', 1);
INSERT INTO `categories` (`c_id`, `c_name`, `c_status`) VALUES(19, 'Altro', 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `categories_places`
--

CREATE TABLE IF NOT EXISTS `categories_places` (
  `cp_category_id` int(11) NOT NULL,
  `cp_place_id` int(11) NOT NULL,
  `cp_contact_email` varchar(255) NOT NULL,
  PRIMARY KEY  (`cp_category_id`,`cp_place_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `categories_places`
--

INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(1, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(2, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(3, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(4, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(5, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(6, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(7, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(8, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(9, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(10, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(11, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(12, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(13, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(14, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(15, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(16, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(17, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(18, 1, '');
INSERT INTO `categories_places` (`cp_category_id`, `cp_place_id`, `cp_contact_email`) VALUES(19, 1, '');

-- --------------------------------------------------------

--
-- Struttura della tabella `configuration_options`
--

CREATE TABLE IF NOT EXISTS `configuration_options` (
  `co_key` varchar(50) NOT NULL,
  `co_value` text NOT NULL,
  PRIMARY KEY  (`co_key`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `configuration_options`
--

INSERT INTO `configuration_options` (`co_key`, `co_value`) VALUES('system_upload_path', '/public/');
INSERT INTO `configuration_options` (`co_key`, `co_value`) VALUES('email_sender_address', 'info@mettiaposto.it');
INSERT INTO `configuration_options` (`co_key`, `co_value`) VALUES('email_sender_name', 'Mettiaposto.it');
INSERT INTO `configuration_options` (`co_key`, `co_value`) VALUES('signal_approve_on_submission', 'True');
INSERT INTO `configuration_options` (`co_key`, `co_value`) VALUES('comment_approve_on_submission', 'True');
INSERT INTO `configuration_options` (`co_key`, `co_value`) VALUES('signal_submission_receiver_address', 'info@mettiaposto.it');

-- --------------------------------------------------------

--
-- Struttura della tabella `places`
--

CREATE TABLE IF NOT EXISTS `places` (
  `p_id` int(11) NOT NULL auto_increment,
  `p_name` varchar(255) NOT NULL,
  `p_status` tinyint(1) NOT NULL,
  PRIMARY KEY  (`p_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dump dei dati per la tabella `places`
--

INSERT INTO `places` (`p_id`, `p_name`, `p_status`) VALUES(1, 'Milano', 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `signals`
--

CREATE TABLE IF NOT EXISTS `signals` (
  `s_id` int(11) NOT NULL auto_increment,
  `s_subject` varchar(255) NOT NULL,
  `s_description` text NOT NULL,
  `s_creation_date` datetime NOT NULL,
  `s_update_date` datetime NOT NULL,
  `s_category_id` smallint(6) NOT NULL,
  `s_email` varchar(255) NOT NULL,
  `s_resolution_date` datetime NOT NULL,
  `s_resolution_description` text NOT NULL,
  `s_address` varchar(255) NOT NULL,
  `s_zip` varchar(5) NOT NULL,
  `s_city` varchar(255) NOT NULL,
  `s_longitude` float(10,6) NOT NULL,
  `s_show_name` tinyint(1) NOT NULL,
  `s_status` int(11) NOT NULL,
  `s_name` varchar(255) NOT NULL,
  `s_latitude` float(10,6) NOT NULL,
  `s_rating` int(11) NOT NULL default '0',
  `s_zoom` int(11) NOT NULL,
  `s_attachment` varchar(255) NOT NULL,
  PRIMARY KEY  (`s_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dump dei dati per la tabella `signals`
--


-- --------------------------------------------------------

--
-- Struttura della tabella `signals_comments`
--

CREATE TABLE IF NOT EXISTS `signals_comments` (
  `sc_signal_id` int(11) NOT NULL,
  `sc_id` int(11) NOT NULL auto_increment,
  `sc_text` text NOT NULL,
  `sc_name` varchar(255) NOT NULL,
  `sc_email` varchar(255) NOT NULL,
  `sc_user_id` int(11) NOT NULL,
  `sc_attachment` varchar(255) NOT NULL,
  `sc_status` int(11) NOT NULL,
  `sc_show_name` tinyint(4) NOT NULL,
  `sc_creation_date` datetime NOT NULL,
  PRIMARY KEY  (`sc_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dump dei dati per la tabella `signals_comments`
--


-- --------------------------------------------------------

--
-- Struttura della tabella `signals_subscriptions`
--

CREATE TABLE IF NOT EXISTS `signals_subscriptions` (
  `ss_signal_id` int(11) NOT NULL,
  `ss_email` varchar(255) NOT NULL,
  PRIMARY KEY  (`ss_signal_id`,`ss_email`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `signals_subscriptions`
--

