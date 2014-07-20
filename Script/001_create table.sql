USE [KN_Order_Storage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

------------------------删除表------------------------

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('rf_role_function_rel') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	ALTER TABLE rf_role_function_rel DROP CONSTRAINT fk_rf_role_function_rel_ro_role_id
	ALTER TABLE rf_role_function_rel DROP CONSTRAINT fk_rf_role_function_rel_fu_function_id
	DROP TABLE [dbo].[rf_role_function_rel]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('ul_userlog') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	ALTER TABLE ul_userlog DROP CONSTRAINT fk_ul_userlog_us_user_id
	ALTER TABLE ul_userlog DROP CONSTRAINT fk_ul_userlog_or_order_id
	DROP TABLE [dbo].[ul_userlog]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('us_user') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	ALTER TABLE us_user DROP CONSTRAINT fk_us_user_ro_role_id
	DROP TABLE [dbo].[us_user]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('st_stock') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	ALTER TABLE st_stock DROP CONSTRAINT fk_st_stock_in_inventory_id
	DROP TABLE [dbo].[st_stock]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('or_order') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	ALTER TABLE or_order DROP CONSTRAINT fk_or_order_ct_client_id
	ALTER TABLE or_order DROP CONSTRAINT fk_or_order_in_inventory_id
	DROP TABLE [dbo].[or_order]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('in_inventory') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	DROP TABLE [dbo].[in_inventory]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('ct_client') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	DROP TABLE [dbo].[ct_client]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('fu_function') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	DROP TABLE [dbo].[fu_function]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('ro_role') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	DROP TABLE [dbo].[ro_role]
END

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('ul_userlog') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)  
BEGIN
	DROP TABLE [dbo].[ul_userlog]
END

GO

------------------------新建表------------------------

--业务表

CREATE TABLE [dbo].[ct_client](
	[ct_client_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL, 	
	[ct_client_source] [nvarchar](50) NULL,
	[ct_client_source_cst] [nvarchar](50) NULL,
	[us_user_name] [nvarchar](50) NOT NULL,
	[ct_create_time] [datetime] NOT NULL,
	[ct_client_name] [nvarchar](50) NOT NULL,
	[ct_status] [nchar](1) NOT NULL,
	[ct_client_tel] [nvarchar](50) NOT NULL,
	[ct_client_qq] [nvarchar](50) NULL,
	[ct_wedding_day] [datetime] NULL,
	[ct_reach_status] [nchar](1) NULL,
	[ct_order_status] [nchar](1) NULL,
	[ct_express] [nvarchar](50) NULL,
	[ct_freight] [float] NULL,
	[ct_address] [nvarchar](500) NULL,
	[ct_remark] [nvarchar](500) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[in_inventory](
	[in_inventory_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[in_inventory_type] [nvarchar](50) NULL,
	[us_user_name] [nvarchar](50) NOT NULL,
	[in_create_time] [datetime] NOT NULL,
	[in_image] [image] NULL,
	[in_style_id] [nvarchar](50) NULL,
	[in_style_name] [nvarchar](50) NULL,
	[in_status] [nchar](1) NOT NULL,	
	[in_color] [nchar](10) NULL,
	[in_size] [nchar](10) NULL,
	[in_eur] [float] NULL,
	[in_usd] [float] NULL,
	[in_cny] [float] NULL,
	[in_amount] [int] NULL,
	[in_remark] [nvarchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[st_stock](
	[st_stock_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[in_inventory_id] [int] NOT NULL,
	[us_user_name] [nvarchar](50) NOT NULL,
	[st_status] [nchar](1) NOT NULL,
	[st_in_number] [int] NULL,
	[st_out_number] [int] NULL,
	[st_in_time] [datetime] NULL,
	[st_out_time] [datetime] NULL,
	[st_remark] [nvarchar](500) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[or_order](
	[or_order_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ct_client_id] [int] NULL,
	[or_order_type] [nchar](10) NOT NULL,
	[in_inventory_id] [int] NULL,
	[or_status] [nchar](1) NOT NULL,
	[or_from] [nvarchar](50) NULL,
	[or_order_time] [datetime] NULL,
	[or_delivered_time] [datetime] NULL,
	[or_style_id] [nvarchar](50) NULL,
	[or_style_name] [nvarchar](50) NULL,
	[or_handled_by] [nvarchar](50) NULL,
	[or_price] [float] NULL,
	[or_amount] [float] NULL,
	[or_remark] [nvarchar](500) NULL,
	[or_s_shoulder] [float] NULL,
	[or_s_chest] [float] NULL,
	[or_s_waist] [float] NULL,
	[or_s_buttocks] [float] NULL,
	[or_s_stature] [float] NULL,
	[or_s_length] [float] NULL,
	[or_s_shoes] [float] NULL,
	[or_order_status] [nchar](1) NULL
) ON [PRIMARY]

ALTER TABLE st_stock
ADD CONSTRAINT fk_st_stock_in_inventory_id FOREIGN KEY (in_inventory_id) REFERENCES in_inventory(in_inventory_id)

ALTER TABLE or_order
ADD CONSTRAINT fk_or_order_ct_client_id FOREIGN KEY (ct_client_id) REFERENCES ct_client(ct_client_id)

ALTER TABLE or_order
ADD CONSTRAINT fk_or_order_in_inventory_id FOREIGN KEY (in_inventory_id) REFERENCES in_inventory(in_inventory_id)

GO

--权限表

CREATE TABLE [dbo].[fu_function](
	[fu_function_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	[fu_function_parent_id] [int] DEFAULT 0,
	[fu_function_name] [nvarchar](50) NOT NULL,
	[fu_function_dec] [nvarchar](500) NULL,
	[fu_controller] [nvarchar](50) NOT NULL,
	[fu_action] [nvarchar](50) NOT NULL,
	[fu_status] [nchar](10) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[ro_role](
	[ro_role_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	[ro_role_name] [nvarchar](50) NOT NULL,
	[ro_function_dec] [nvarchar](500) NULL,
	[ro_status] [nchar](10) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[rf_role_function_rel](
	[rf_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	[ro_role_id] [int] NOT NULL,
	[fu_function_id] [int] NOT NULL,
	[rf_status] [nchar](10) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[us_user](
	[us_user_id] [nvarchar](50) PRIMARY KEY NOT NULL, 
	[ro_role_id] [int] NOT NULL,
	[us_user_name] [nvarchar](50) NOT NULL,
	[us_user_password] [nvarchar](50) NOT NULL,
	[us_from] [nvarchar](50) NULL,
	[us_status] [nchar](10) NOT NULL,
	[us_create_time] [datetime] NOT NULL,
	[us_last_login_time] [datetime] NOT NULL,
	[us_last_login_ip] [nvarchar](255) NOT NULL,
	[us_remark] [nvarchar](500) NULL
) ON [PRIMARY]

ALTER TABLE rf_role_function_rel
ADD CONSTRAINT fk_rf_role_function_rel_ro_role_id FOREIGN KEY (ro_role_id) REFERENCES ro_role(ro_role_id)

ALTER TABLE rf_role_function_rel
ADD CONSTRAINT fk_rf_role_function_rel_fu_function_id FOREIGN KEY (fu_function_id) REFERENCES fu_function(fu_function_id)

ALTER TABLE us_user
ADD CONSTRAINT fk_us_user_ro_role_id FOREIGN KEY (ro_role_id) REFERENCES ro_role(ro_role_id)

--Log表
CREATE TABLE [dbo].[ul_userlog](
	[ul_userlog_id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	[us_user_id] [nvarchar](50) NOT NULL,
	[or_order_id] [int] NOT NULL,
	[ul_log_time] [datetime] NOT NULL,
	[ul_operation] [nvarchar](10) NULL,
	[ul_description] [nvarchar](500) NULL,
) ON [PRIMARY]

ALTER TABLE ul_userlog
ADD CONSTRAINT fk_ul_userlog_or_order_id FOREIGN KEY (or_order_id) REFERENCES or_order(or_order_id)

ALTER TABLE ul_userlog
ADD CONSTRAINT fk_ul_userlog_us_user_id FOREIGN KEY (us_user_id) REFERENCES us_user(us_user_id)






