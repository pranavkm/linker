﻿using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mono.Linker.Tests.Cases.DataFlow
{
	[SetupCSharpCompilerToUse ("csc")]
	[KeptMember (".ctor()")]
	public class MethodReturnParameterDataFlow
	{
		public static void Main()
		{
			var instance = new MethodReturnParameterDataFlow ();

			// Validation that assigning value to the return value is verified
			NoRequirements ();
			instance.ReturnDefaultConstructor (typeof (TestType), typeof (TestType), typeof (TestType));
			instance.ReturnDefaultConstructorFromUnknownType (null);
			instance.ReturnDefaultConstructorFromConstant ();
			instance.ReturnDefaultConstructorFromNull ();
			instance.ReturnPublicConstructorsFailure (null);
			instance.ReturnConstructorsFailure (null);

			// Validation that value comming from return value of a method is correctly propagated
			instance.PropagateReturnDefaultConstructor ();
			instance.PropagateReturnDefaultConstructorFromConstant ();
		}

		[Kept]
		private static Type NoRequirements ()
		{
			return typeof (TestType);
		}

		[RecognizedReflectionAccessPattern]
		[Kept]
		[return: DynamicallyAccessedMembers (DynamicallyAccessedMemberKinds.DefaultConstructor)]
		[return: KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
		private Type ReturnDefaultConstructor (
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberKinds.DefaultConstructor)]
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			Type defaultConstructorType,
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberKinds.PublicConstructors)]
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			Type publicConstructorsType,
			[DynamicallyAccessedMembers (DynamicallyAccessedMemberKinds.Constructors)]
			[KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
			Type constructorsType)
		{
			switch (GetHashCode ()) {
				case 1:
					return defaultConstructorType;
				case 2:
					return publicConstructorsType;
				case 3:
					return constructorsType;
				case 4:
					return typeof (TestType);
				default:
					return null;
			}
		}

		[UnrecognizedReflectionAccessPattern (typeof (MethodReturnParameterDataFlow), nameof (ReturnDefaultConstructorFromUnknownType), new Type [] { typeof (Type) })]
		[Kept]
		[return: DynamicallyAccessedMembers (DynamicallyAccessedMemberKinds.DefaultConstructor)]
		[return: KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
		private Type ReturnDefaultConstructorFromUnknownType (Type unknownType)
		{
			return unknownType;
		}

		[RecognizedReflectionAccessPattern]
		[Kept]
		[return: DynamicallyAccessedMembers (DynamicallyAccessedMemberKinds.DefaultConstructor)]
		[return: KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
		private Type ReturnDefaultConstructorFromConstant ()
		{
			return typeof (TestType);
		}

		[RecognizedReflectionAccessPattern]
		[Kept]
		[return: DynamicallyAccessedMembers (DynamicallyAccessedMemberKinds.DefaultConstructor)]
		[return: KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
		private Type ReturnDefaultConstructorFromNull ()
		{
			return null;
		}

		[UnrecognizedReflectionAccessPattern (typeof (MethodReturnParameterDataFlow), nameof (ReturnPublicConstructorsFailure), new Type [] { typeof (Type) })]
		[Kept]
		[return: DynamicallyAccessedMembers (DynamicallyAccessedMemberKinds.PublicConstructors)]
		[return: KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
		private Type ReturnPublicConstructorsFailure (
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberKinds.DefaultConstructor)]
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			Type defaultConstructorType)
		{
			return defaultConstructorType;
		}

		[UnrecognizedReflectionAccessPattern (typeof (MethodReturnParameterDataFlow), nameof (ReturnConstructorsFailure), new Type [] { typeof (Type) })]
		[Kept]
		[return: DynamicallyAccessedMembers (DynamicallyAccessedMemberKinds.Constructors)]
		[return: KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
		private Type ReturnConstructorsFailure (
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberKinds.PublicConstructors)]
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			Type publicConstructorsType)
		{
			return publicConstructorsType;
		}

		[Kept]
		[UnrecognizedReflectionAccessPattern (typeof (MethodReturnParameterDataFlow), nameof (RequirePublicConstructors), new Type [] { typeof (Type) })]
		[UnrecognizedReflectionAccessPattern (typeof (MethodReturnParameterDataFlow), nameof (RequireConstructors), new Type [] { typeof (Type) })]
		private void PropagateReturnDefaultConstructor()
		{
			Type t = ReturnDefaultConstructor (typeof (TestType), typeof (TestType), typeof (TestType));
			RequireDefaultConstructor (t);
			RequirePublicConstructors (t);
			RequireConstructors (t);
			RequireNothing (t);
		}

		[Kept]
		[UnrecognizedReflectionAccessPattern (typeof (MethodReturnParameterDataFlow), nameof (RequirePublicConstructors), new Type [] { typeof (Type) })]
		[UnrecognizedReflectionAccessPattern (typeof (MethodReturnParameterDataFlow), nameof (RequireConstructors), new Type [] { typeof (Type) })]
		private void PropagateReturnDefaultConstructorFromConstant ()
		{
			Type t = ReturnDefaultConstructorFromConstant ();
			RequireDefaultConstructor (t);
			RequirePublicConstructors (t);
			RequireConstructors (t);
			RequireNothing (t);
		}

		[Kept]
		private static void RequireDefaultConstructor (
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberKinds.DefaultConstructor)]
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			Type type)
		{
		}

		[Kept]
		private static void RequirePublicConstructors (
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberKinds.PublicConstructors)]
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			Type type)
		{
		}

		[Kept]
		private static void RequireConstructors (
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberKinds.Constructors)]
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			Type type)
		{
		}

		[Kept]
		private static void RequireNothing (Type type)
		{
		}

		[Kept]
		class TestType
		{
			[Kept]
			public TestType () { }
		}
	}
}
