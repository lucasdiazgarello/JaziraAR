#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>



// System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection>
struct Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079;
// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IMetric`1<System.Double>>
struct IReadOnlyDictionary_2_t7E602F68B800E9FD97AE5462BAAEB2B2A0A6A6FD;
// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IMetric`1<System.Int64>>
struct IReadOnlyDictionary_2_t1C9E4278DD0992BFB53341CF2B1D31E871D5CB59;
// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IMetric`1<System.TimeSpan>>
struct IReadOnlyDictionary_2_t727071262B8B87C9F1C794CCE7F03282F35F8F4C;
// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IEventMetric>
struct IReadOnlyDictionary_2_tC190C0DF3FC65E46AC2382576C1FF9A793C41951;
// System.Collections.Generic.IReadOnlyList`1<Unity.Multiplayer.Tools.NetStats.IMetric>
struct IReadOnlyList_1_tACBB2E1C257871194507D0C10E16FD03C486E45A;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// Unity.Multiplayer.Tools.NetStats.MetricCollection
struct MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA;
// Unity.Multiplayer.Tools.MetricObserver
struct MetricObserver_t69990AE7E36765C6F5F9108572F3D1588AD9DE3B;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tB3F3C08E56AA3ACF739E1A4E522FECE15EDE914C 
{
};
struct Il2CppArrayBounds;

// Unity.Multiplayer.Tools.NetStats.MetricCollection
struct MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA  : public RuntimeObject
{
	// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IMetric`1<System.Int64>> Unity.Multiplayer.Tools.NetStats.MetricCollection::m_Counters
	RuntimeObject* ___m_Counters_0;
	// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IMetric`1<System.Double>> Unity.Multiplayer.Tools.NetStats.MetricCollection::m_Gauges
	RuntimeObject* ___m_Gauges_1;
	// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IMetric`1<System.TimeSpan>> Unity.Multiplayer.Tools.NetStats.MetricCollection::m_Timers
	RuntimeObject* ___m_Timers_2;
	// System.Collections.Generic.IReadOnlyDictionary`2<Unity.Multiplayer.Tools.NetStats.MetricId,Unity.Multiplayer.Tools.NetStats.IEventMetric> Unity.Multiplayer.Tools.NetStats.MetricCollection::m_PayloadEvents
	RuntimeObject* ___m_PayloadEvents_3;
	// System.Collections.Generic.IReadOnlyList`1<Unity.Multiplayer.Tools.NetStats.IMetric> Unity.Multiplayer.Tools.NetStats.MetricCollection::<Metrics>k__BackingField
	RuntimeObject* ___U3CMetricsU3Ek__BackingField_4;
	// System.UInt64 Unity.Multiplayer.Tools.NetStats.MetricCollection::<ConnectionId>k__BackingField
	uint64_t ___U3CConnectionIdU3Ek__BackingField_5;
};

// Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher
struct MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011  : public RuntimeObject
{
};

struct MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_StaticFields
{
	// System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection> Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::OnMetricsReceived
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* ___OnMetricsReceived_0;
};

// Unity.Multiplayer.Tools.MetricObserver
struct MetricObserver_t69990AE7E36765C6F5F9108572F3D1588AD9DE3B  : public RuntimeObject
{
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection>
struct Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079  : public MulticastDelegate_t
{
};

// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87  : public MulticastDelegate_t
{
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif


// System.Void System.Action`1<System.Object>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) ;

// System.Void Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::RaiseOnMetricsReceived(Unity.Multiplayer.Tools.NetStats.MetricCollection)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MetricEventPublisher_RaiseOnMetricsReceived_mFBC4AFBAC22CE751930790E53E29ADF7648BBB79 (MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA* ___metricCollection0, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Combine(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00 (Delegate_t* ___a0, Delegate_t* ___b1, const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Remove(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3 (Delegate_t* ___source0, Delegate_t* ___value1, const RuntimeMethod* method) ;
// System.Void System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection>::Invoke(T)
inline void Action_1_Invoke_m4036EB3B05642DFF32D8DBE1F4AEB7A33205CD2E_inline (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* __this, MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA* ___obj0, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*, MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA*, const RuntimeMethod*))Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline)(__this, ___obj0, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Unity.Multiplayer.Tools.MetricObserver::Observe(Unity.Multiplayer.Tools.NetStats.MetricCollection)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MetricObserver_Observe_mB28F8E3DDD32221B1A610630AEF42500EFD322A5 (MetricObserver_t69990AE7E36765C6F5F9108572F3D1588AD9DE3B* __this, MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA* ___collection0, const RuntimeMethod* method) 
{
	{
		// MetricEventPublisher.RaiseOnMetricsReceived(collection);
		MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA* L_0 = ___collection0;
		MetricEventPublisher_RaiseOnMetricsReceived_mFBC4AFBAC22CE751930790E53E29ADF7648BBB79(L_0, NULL);
		// }
		return;
	}
}
// System.Void Unity.Multiplayer.Tools.MetricObserver::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MetricObserver__ctor_mD010C3261BC937F926D5CF0BDC1C1410F22BEB1C (MetricObserver_t69990AE7E36765C6F5F9108572F3D1588AD9DE3B* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::add_OnMetricsReceived(System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MetricEventPublisher_add_OnMetricsReceived_m2307BDA3A26A1EE1D7C51E0C5772499048E2FAEB (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* V_0 = NULL;
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* V_1 = NULL;
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* V_2 = NULL;
	{
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_0 = ((MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_StaticFields*)il2cpp_codegen_static_fields_for(MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var))->___OnMetricsReceived_0;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_1 = V_0;
		V_1 = L_1;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_2 = V_1;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_3 = ___value0;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)Castclass((RuntimeObject*)L_4, Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var));
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_5 = V_2;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_6 = V_1;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*>((&((MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_StaticFields*)il2cpp_codegen_static_fields_for(MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var))->___OnMetricsReceived_0), L_5, L_6);
		V_0 = L_7;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_8 = V_0;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)L_8) == ((RuntimeObject*)(Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// System.Void Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::remove_OnMetricsReceived(System.Action`1<Unity.Multiplayer.Tools.NetStats.MetricCollection>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MetricEventPublisher_remove_OnMetricsReceived_m020640DD0D777A4C7C81E9F331B3421CDB90D976 (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* V_0 = NULL;
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* V_1 = NULL;
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* V_2 = NULL;
	{
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_0 = ((MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_StaticFields*)il2cpp_codegen_static_fields_for(MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var))->___OnMetricsReceived_0;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_1 = V_0;
		V_1 = L_1;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_2 = V_1;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_3 = ___value0;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)Castclass((RuntimeObject*)L_4, Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var));
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_5 = V_2;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_6 = V_1;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*>((&((MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_StaticFields*)il2cpp_codegen_static_fields_for(MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var))->___OnMetricsReceived_0), L_5, L_6);
		V_0 = L_7;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_8 = V_0;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)L_8) == ((RuntimeObject*)(Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// System.Void Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher::RaiseOnMetricsReceived(Unity.Multiplayer.Tools.NetStats.MetricCollection)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MetricEventPublisher_RaiseOnMetricsReceived_mFBC4AFBAC22CE751930790E53E29ADF7648BBB79 (MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA* ___metricCollection0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* G_B2_0 = NULL;
	Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* G_B1_0 = NULL;
	{
		// OnMetricsReceived?.Invoke(metricCollection);
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_0 = ((MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_StaticFields*)il2cpp_codegen_static_fields_for(MetricEventPublisher_t8D8F8038DFF35E30F7C85880899E9C39B3B7F011_il2cpp_TypeInfo_var))->___OnMetricsReceived_0;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000a;
		}
	}
	{
		return;
	}

IL_000a:
	{
		MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA* L_2 = ___metricCollection0;
		NullCheck(G_B2_0);
		Action_1_Invoke_m4036EB3B05642DFF32D8DBE1F4AEB7A33205CD2E_inline(G_B2_0, L_2, NULL);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___obj0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
