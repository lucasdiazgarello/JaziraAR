#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.Void Unity.Multiplayer.Tools.MetricObserver::Observe(Unity.Multiplayer.Tools.NetStats.MetricCollection)
extern void MetricObserver_Observe_mB28F8E3DDD32221B1A610630AEF42500EFD322A5 (void);
// 0x00000002 System.Void Unity.Multiplayer.Tools.MetricObserver::.ctor()
extern void MetricObserver__ctor_mD010C3261BC937F926D5CF0BDC1C1410F22BEB1C (void);
// 0x00000003 System.Void Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::add_OnMetricsReceived(System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection>)
extern void MetricEventPublisher_add_OnMetricsReceived_m2307BDA3A26A1EE1D7C51E0C5772499048E2FAEB (void);
// 0x00000004 System.Void Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::remove_OnMetricsReceived(System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection>)
extern void MetricEventPublisher_remove_OnMetricsReceived_m020640DD0D777A4C7C81E9F331B3421CDB90D976 (void);
// 0x00000005 System.Void Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::RaiseOnMetricsReceived(Unity.Multiplayer.Tools.NetStats.MetricCollection)
extern void MetricEventPublisher_RaiseOnMetricsReceived_mFBC4AFBAC22CE751930790E53E29ADF7648BBB79 (void);
static Il2CppMethodPointer s_methodPointers[5] = 
{
	MetricObserver_Observe_mB28F8E3DDD32221B1A610630AEF42500EFD322A5,
	MetricObserver__ctor_mD010C3261BC937F926D5CF0BDC1C1410F22BEB1C,
	MetricEventPublisher_add_OnMetricsReceived_m2307BDA3A26A1EE1D7C51E0C5772499048E2FAEB,
	MetricEventPublisher_remove_OnMetricsReceived_m020640DD0D777A4C7C81E9F331B3421CDB90D976,
	MetricEventPublisher_RaiseOnMetricsReceived_mFBC4AFBAC22CE751930790E53E29ADF7648BBB79,
};
static const int32_t s_InvokerIndices[5] = 
{
	7566,
	9591,
	15792,
	15792,
	15792,
};
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_Unity_Multiplayer_Tools_MetricEvents_CodeGenModule;
const Il2CppCodeGenModule g_Unity_Multiplayer_Tools_MetricEvents_CodeGenModule = 
{
	"Unity.Multiplayer.Tools.MetricEvents.dll",
	5,
	s_methodPointers,
	0,
	NULL,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
