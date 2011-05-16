/* ===============================================================================
   Cygnus Innovation Ltd.
 * Designed By Md. Ataur Rahaman
 * 
 * 
 * Descripttion: This is the ....
// ==============================================================================*/


using System;
using System.Collections.Generic;
using System.Text;

namespace AlarmasABC.DAL
{
    class StoredProcedure
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Name
        {
			// -- Company Functions -- //
            SP_INSERT_COMPANY,
            SP_SELECT_COMPANY,
            SP_UPDATE_COMPANY,
            SP_DELETE_COMPANY,
			
			// -- User Group Functions -- //
            SP_INSERT_USER_GROUP,
            SP_SELECT_USER_GROUP,
            SP_UPDATE_USER_GROUP,
            SP_DELETE_USER_GROUP,
			
			// -- User Functions -- //
            SP_INSERT_USER,
            SP_SELECT_USER,
            SP_UPDATE_USER,
			
			// -- Unit Group Functions -- //
            SP_INSERT_USER_UNITGROUP,
            SP_SELECT_LISTEDUNITGROUP,
            SP_SELECT_NOTLISTEDUNITGROUP,
            SP_DELETE_UNIT_GROUP,
			
			// -- Unit Model Functions -- //
            SP_INSERT_UNIT_MODEL,
            SP_SELECT_UNIT_MODEL,
            SP_DELETE_UNIT_MODEL,
            SP_UPDATE_UNIT_MODEL,
			
			// -- Userwise Unit Category Functions -- //
            SP_INSERT_USERWISE_UNITCAT,
            SP_DELETE_USERWISE_UNITCAT,
			
            SP_DELETE_UNIT_USERWISE,
			
			// -- Unit Geofence Functions -- //
            SP_INSERT_UNIT_GEOFENCE,
            SP_DELETE_UNIT_GEOFENCE,
			
			// -- Unit Group Geofence Functions -- //
            SP_INSERT_UNIT_GROUP_GEOFENCE,
			
			// -- Icon Functions -- //
            SP_INSERT_ICON,
            SP_SELECT_ICON,
            SP_DELETE_ICON,
			
			// -- Rules Functions -- //
            SP_INSERT_RULES,
            SP_SELECT_RULES,
			
			// -- Unitwise Rules Functions -- //
            SP_INSERT_RULES_UNITWISE,
			
            CREATESUPPLIES,
            CREATEPATTERN,
            DISRTANCECALCULATOR,
            GROUPWISEUNIT,
            SP_INSERT_UNIT_GROUP,
            SP_CREATERULES,
            SP_CREATEUSER,
            SP_CREATE_GEOFENCEUNITGROUPWISE,
            SP_CRGEOFENCEUNITWISE,
            SP_DELETE_SCHEME,
            SP_INSERTSPEED,
            SP_SAVEBOOKMARK,
            SP_UPDATE_SCHEME_INFO,
            SP_UPDATE_SCHEME_PERMISSION,
            SP_TIMEALERT,
            SP_UPDATEGEOFENCEUNITWISE,
            SP_UPDATEUSER,
            SP_UPDATEUSERGROUP,
            SP_USERGROUP,
            SP_UPDATE_USER_GROUPWISE_SCHEME,
            SP_UPDATE_USERWISE_SCHEME,
            SPCONVERTSECONDSTODATETIME,
            SPRULESADD,
            SPUNITTYPEADD,
            SP_INSERT_VEHICLE,
            SP_UPDATE_VEHICLE,
            UPDATEPATTERN,
            SP_INSERT_UNIT_TYPE,
            SP_DELETE_UNIT_TYPE,
            SP_SELECT_UNIT_TYPE,
            SP_SELECT_FUEL,
            SP_DDL_SELECT_USER,
            SP_DDL_SELECT_USERGROUP,
            SP_UPDATE_UNIT_TYPE,
            SP_USER_LOGIN,
            SP_DDL_UNITTYPE, 
            SP_SELECT_GROUPLIST,
            SP_SELECT_UNIT,
            SP_SELECT_MAINMAP_DATA,
            SP_SELECT_UNIT_INFO,
			
            FN_SELECT_MAINMAP_DATA,
			
			SP_INSERT_UNIT,
            SP_SELECT_LOADLIST,
            SP_SELECT_LOADIMAGE,
            SP_INSERT_UNIT_USER,
            SP_CHECK_INFO,
            SP_TREE_DATA_SELECT,
            SP_DELETE_UNIT_INFO,
            SP_SELECT_UNITCOUNT,
            SP_SELECT_AD_UNITMODEL,
            SP_SELECT_MAINMAP_DATA_UNITTYPE,
            FN_SELECT_MAINMAP_DATA_UNITTYPE,
            SP_SELECT_MINIMAP_DATA,
            SP_UPDATE_UNIT_USER,
            SP_DELETE_UNIT_USER,
            SP_SELECT_DEVICEID,
            SP_INSERT_UNITGROUP_USER,
            SP_SELECT_PATTERN,
            SP_SELECT_NOTLISTEDGROUP,
            SP_SELECT_UNITS_COMPANY,
            SP_SELECT_IMAGE,
            SP_INSERT_USER_UNIT,
            SP_SELECT_MAXUNITID,
            SP_SELECT_DATA_BREADCRAMS1,
            SP_SELECT_DATA_BREADCRAMS2,
            SP_UPDATE_UNIT_USERWISE,
            SP_INSERT_UNIT_USERWISE,
            insertUnitGroup,
            SP_UNIT_HISTORICALDATA,
            SP_SAFETYZONESLIST_SELECT,
            SP_SELECT_COMPANY_UNIT_TYPE,
            SP_SELECT_SPEEDING_RULES,
            SP_INSERT_SPEEDING_DATA,
            SP_UPDATE_SPEEDING_DATA,
            SP_SELECT_LISTEDUNITS,
            SP_SELECT_NOTLISTEDUNITS,
            SP_SELECT_TREECOLOR_1,
            SP_SELECT_TREEcOLOR_2,
            SP_SELECT_TREEcOLOR_3,
            SP_SELECT_TREEcOLOR_4,
            SP_SELECT_ERROR_REPORT,
            SP_SELECT_USERGROUP_COMPANYWISE,
            SP_SELECT_LISTEDGROUP,
            SP_UPDATE_GEOFENCE_UNITWISE,
            SP_INSERT_GEOFENCE_UNITWISE,
            SP_SELECT_LOADSCHEME,
            SP_SELECT_SCHEME_INFO,
			SP_INSERT_USER_GROUPWISE_UNIT,
            SP_SELECT_MODULE,
            SP_SELECT_SCHEMEUNITCOUNT,
            SP_SELECT_SCHEMEUSERCOUNT,
            SP_SELECT_NOT_USER_GROUP,
            SP_SELECT_LISTED_USER,
            SP_SELECT_NOT_LISTED_USER,
            SP_SELECT_GEOFENCE_DATA,
            SP_SELECT_LOADMODULE,
            SP_GroupWISESCHEME,
            SP_SELECTUSERLIST,
            SP_SELECT_SUPPLIES,
            SP_INSERT_SUPPLIES,
            SP_UPDATE_SUPPLIES, 
            SP_DELETE_SUPPLIES,
            SP_DELETE_PATTERN,
            SP_SELECT_UNIT_PER_PATTERN,
            SP_SELECT_SUPPLIES_PER_PATTERN,
            SP_LOADLISTEDUNITS_FOR_SUPPLIES,
            SP_LOADNOTLISTEDUNITS_FOR_SUPPLIES,
            SP_DELETEANDUPDATE_PATTERN_PER_UNIT,
            SP_DELETE_PATTERN_PER_UNIT,
            SP_SELECT_GEOFENCE_NAME,
            SP_UPDATE_UNIT,
            SP_INSERT_SUPPLIES_PER_PATTERN,
            SP_INSERT_PATTERN,
            SP_SELECT_PATTERN_UNITS,
            SP_SELECT_UNITS_COMWISE,
            SP_UPDATE_PATTREN,
            SP_DELETE_SUPPLIES_PER_PATTERN,
            SP_SELECT_UNASSIGN_UNITS,
            SP_UPDATE_UNITS_PATTERN,
            SP_SELECT_UNASSIGN_UNITS_3,
            SP_SELECT_SECURITYQUESTION,
            SP_UNIT_COUNT_SELECT,
            SP_SELECT_MANINTAINNANCE_STATUS,
            SP_SELECT_SUPP_PATERN,
            SP_SELECT_GPRS,
            SP_RETRIVE_PASSWORD,
            SP_SELECT_RPT_TIMEZONE,
            SP_UPDATE_RPT_TIMEZONE,
            SP_DELETE_RPT_TIMEZONE,
            SP_INSERT_RPT_TIMEZONE,
            SP_SELECT_TIMEZONE,
            SP_UPDATE_TIMEZONE,
            SP_SELECT_USERS,
            SP_SELECT_ASSIGNED_UNITS,
            SP_LOGIN_ADMIN,
            SP_SELECT_UNITS_Rules_Data,
            SP_DELETE_RULES_DATA,
            SP_UPDATE_RULES_DATA,
            SP_SELECT_SCHEMEPERMISSION,
            SP_SELECT_MODULEALL,
            SP_ADDVIPERACCOUNT,
            SP_INSERT_IMAGE_LOCATION,
            SP_SELECT_IMAGE_INFO,
            SP_UPDATE_IMAGE_LOCATION,
            SP_DELETE_IMAGE_INFO,
            SP_SELECT_ACTIVE_IMAGE_INFO,
            SP_DELETE_GPRS_DATA,
            SP_SELECT_OUTSIDEMAIL_STATUS,
            SP_SELECT_SECURITY_SCHEME,
            SP_DELETE_UNIT,
            SP_DELETE_USER,
            SP_SELECT_ALL_EVENTS,
            SP_INSERT_CONTACT_INFO,
            SP_USER_DISABLE,
            SP_UNIT_ENABLE,
            SP_UNIT_DISABLE,
            SP_CHECK_INFO_FOR_UPDATE,
			FN_INSERT_UNIT_COMMAND
        }
    }
}
