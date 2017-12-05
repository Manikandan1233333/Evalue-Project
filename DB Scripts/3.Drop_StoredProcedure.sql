USE [Payments]
GO

/****** Object:  StoredProcedure [dbo].[CYBERDOWNLOAD_GET_FILE]    Script Date: 4/5/2016 12:39:54 AM ******/
DROP PROCEDURE [dbo].[CYBERDOWNLOAD_GET_FILE]
GO

/****** Object:  StoredProcedure [dbo].[CYBERDOWNLOADFILE_IMPORT_DATA]    Script Date: 4/5/2016 2:45:31 AM ******/
DROP PROCEDURE [dbo].[CYBERDOWNLOADFILE_IMPORT_DATA]
GO

/****** Object:  StoredProcedure [dbo].[CYBER_DownloadResult]    Script Date: 4/5/2016 2:46:22 AM ******/
DROP PROCEDURE [dbo].[CYBER_DownloadResult]
GO

/****** Object:  StoredProcedure [dbo].[CYBER_Export]    Script Date: 4/5/2016 2:46:46 AM ******/
DROP PROCEDURE [dbo].[CYBER_Export]
GO

/****** Object:  StoredProcedure [dbo].[CYBER_Get_File]    Script Date: 4/5/2016 2:47:08 AM ******/
DROP PROCEDURE [dbo].[CYBER_Get_File]
GO

/****** Object:  StoredProcedure [dbo].[CYBER_Get_Url]    Script Date: 4/5/2016 2:47:32 AM ******/
DROP PROCEDURE [dbo].[CYBER_Get_Url]
GO

/****** Object:  StoredProcedure [dbo].[CYBER_Import_Data_renamed]    Script Date: 4/5/2016 2:47:51 AM ******/
DROP PROCEDURE [dbo].[CYBER_Import_Data_renamed]
GO

/****** Object:  StoredProcedure [dbo].[CYBER_Url]    Script Date: 4/5/2016 2:48:10 AM ******/
DROP PROCEDURE [dbo].[CYBER_Url]
GO

/****** Object:  StoredProcedure [dbo].[CYBERDOWNLOAD_GET_FILE_2]    Script Date: 4/5/2016 2:48:32 AM ******/
DROP PROCEDURE [dbo].[CYBERDOWNLOAD_GET_FILE_2]
GO

/****** Object:  StoredProcedure [dbo].[CrossReference_Report_old]    Script Date: 4/6/2016 3:42:30 AM ******/
DROP PROCEDURE [dbo].[CrossReference_Report_old]
GO

/****** Object:  StoredProcedure [dbo].[DeleteDuplicateRecords_Reports_Table]    Script Date: 4/6/2016 3:46:30 AM ******/
DROP PROCEDURE [dbo].[DeleteDuplicateRecords_Reports_Table]
GO

/****** Object:  StoredProcedure [dbo].[Get_Check_IVR_Policy_old]    Script Date: 4/6/2016 3:52:39 AM ******/
DROP PROCEDURE [dbo].[Get_Check_IVR_Policy_old]
GO

/****** Object:  StoredProcedure [dbo].[Get_INS_MBR_StatusByDesc]    Script Date: 4/6/2016 3:54:19 AM ******/
DROP PROCEDURE [dbo].[Get_INS_MBR_StatusByDesc]
GO

/****** Object:  StoredProcedure [dbo].[INS_AutoHO_Batch]    Script Date: 4/6/2016 3:55:56 AM ******/
DROP PROCEDURE [dbo].[INS_AutoHO_Batch]
GO

/****** Object:  StoredProcedure [dbo].[INS_AutoHO_HUON_Batch]    Script Date: 4/6/2016 3:56:55 AM ******/
DROP PROCEDURE [dbo].[INS_AutoHO_HUON_Batch]
GO

/****** Object:  StoredProcedure [dbo].[INS_Check_Duplicate_Payment]    Script Date: 4/6/2016 4:21:23 AM ******/
DROP PROCEDURE [dbo].[INS_Check_Duplicate_Payment]
GO

/****** Object:  StoredProcedure [dbo].[INS_Check_IVR_Policy]    Script Date: 4/6/2016 4:26:15 AM ******/
DROP PROCEDURE [dbo].[INS_Check_IVR_Policy]
GO

/****** Object:  StoredProcedure [dbo].[INS_Check_IVR_Policy_bf1211]    Script Date: 4/6/2016 4:28:36 AM ******/
DROP PROCEDURE [dbo].[INS_Check_IVR_Policy_bf1211]
GO

/****** Object:  StoredProcedure [dbo].[INS_Check_IVR_Policy_BK]    Script Date: 4/6/2016 4:29:35 AM ******/
DROP PROCEDURE [dbo].[INS_Check_IVR_Policy_BK]
GO

/****** Object:  StoredProcedure [dbo].[INS_Check_IVR_Policy_orig]    Script Date: 4/6/2016 4:30:19 AM ******/
DROP PROCEDURE [dbo].[INS_Check_IVR_Policy_orig]
GO

/****** Object:  StoredProcedure [dbo].[INS_Check_IVR_Policy_TEST]    Script Date: 4/6/2016 4:30:56 AM ******/
DROP PROCEDURE [dbo].[INS_Check_IVR_Policy_TEST]
GO

/****** Object:  StoredProcedure [dbo].[INS_HUON_RBE_Batch]    Script Date: 4/6/2016 4:33:08 AM ******/
DROP PROCEDURE [dbo].[INS_HUON_RBE_Batch]
GO

/****** Object:  StoredProcedure [dbo].[INS_RBE_Batch]    Script Date: 4/6/2016 4:34:33 AM ******/
DROP PROCEDURE [dbo].[INS_RBE_Batch]
GO

/****** Object:  StoredProcedure [dbo].[INS_RBE_Batch_Prod]    Script Date: 4/6/2016 4:35:04 AM ******/
DROP PROCEDURE [dbo].[INS_RBE_Batch_Prod]
GO

/****** Object:  StoredProcedure [dbo].[MEM_Reports]    Script Date: 4/6/2016 4:36:49 AM ******/
DROP PROCEDURE [dbo].[MEM_Reports]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Batch_Reports]    Script Date: 4/6/2016 4:42:18 AM ******/
DROP PROCEDURE [dbo].[PAY_Batch_Reports]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Exigen_Batch_Report]    Script Date: 4/6/2016 4:45:54 AM ******/
DROP PROCEDURE [dbo].[PAY_Exigen_Batch_Report]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Get_POESPayments]    Script Date: 4/6/2016 4:47:24 AM ******/
DROP PROCEDURE [dbo].[PAY_Get_POESPayments]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Get_Remittance_Feed]    Script Date: 4/6/2016 4:48:28 AM ******/
DROP PROCEDURE [dbo].[PAY_Get_Remittance_Feed]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Get_Remittance_PaymentCentral]    Script Date: 4/6/2016 4:49:12 AM ******/
DROP PROCEDURE [dbo].[PAY_Get_Remittance_PaymentCentral]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Get_Remittance_PUP]    Script Date: 4/6/2016 4:49:56 AM ******/
DROP PROCEDURE [dbo].[PAY_Get_Remittance_PUP]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Get_Remittance_PUPSAMPLE]    Script Date: 4/6/2016 4:50:29 AM ******/
DROP PROCEDURE [dbo].[PAY_Get_Remittance_PUPSAMPLE]
GO

/****** Object:  StoredProcedure [dbo].[PAY_Get_Remittance_SIS]    Script Date: 4/6/2016 4:51:23 AM ******/
DROP PROCEDURE [dbo].[PAY_Get_Remittance_SIS]
GO

/****** Object:  StoredProcedure [dbo].[PAY_HUON_Batch_Report]    Script Date: 4/6/2016 4:52:18 AM ******/
DROP PROCEDURE [dbo].[PAY_HUON_Batch_Report]
GO

/****** Object:  StoredProcedure [dbo].[Pay_Payment_Refund]    Script Date: 4/6/2016 4:54:00 AM ******/
DROP PROCEDURE [dbo].[Pay_Payment_Refund]
GO

/****** Object:  StoredProcedure [dbo].[PAY_POES_Batch_Report]    Script Date: 4/6/2016 4:55:16 AM ******/
DROP PROCEDURE [dbo].[PAY_POES_Batch_Report]
GO


/****** Object:  StoredProcedure [dbo].[PAY_SIS_Batch_Reports]    Script Date: 4/6/2016 4:57:34 AM ******/
DROP PROCEDURE [dbo].[PAY_SIS_Batch_Reports]
GO

/****** Object:  StoredProcedure [dbo].[POES_RBE_MONITORING]    Script Date: 4/6/2016 4:58:51 AM ******/
DROP PROCEDURE [dbo].[POES_RBE_MONITORING]
GO

/****** Object:  StoredProcedure [dbo].[PS_Batch]    Script Date: 4/6/2016 5:00:13 AM ******/
DROP PROCEDURE [dbo].[PS_Batch]
GO

/****** Object:  StoredProcedure [dbo].[PS_Batch_Club]    Script Date: 4/6/2016 5:00:45 AM ******/
DROP PROCEDURE [dbo].[PS_Batch_Club]
GO

/****** Object:  StoredProcedure [dbo].[PS_GL_Batch_Exigen]    Script Date: 4/6/2016 5:01:16 AM ******/
DROP PROCEDURE [dbo].[PS_GL_Batch_Exigen]
GO

/****** Object:  StoredProcedure [dbo].[PS_GL_Batch_Exigen_Club]    Script Date: 4/6/2016 5:01:58 AM ******/
DROP PROCEDURE [dbo].[PS_GL_Batch_Exigen_Club]
GO