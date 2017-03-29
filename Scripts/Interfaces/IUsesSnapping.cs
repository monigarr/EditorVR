﻿using System;
using UnityEngine;

namespace UnityEditor.Experimental.EditorVR
{
	/// <summary>
	/// Gives decorated class the ability to use snapping
	/// </summary>
	public interface IUsesSnapping
	{
	}

	public static class IUsesSnappingMethods
	{
		internal delegate bool TransformWithSnappingDelegate(Transform rayOrigin, GameObject[] objects, ref Vector3 position, ref Quaternion rotation, Vector3 delta);
		internal delegate bool DirectTransformWithSnappingDelegate(Transform rayOrigin, GameObject[] objects, ref Vector3 position, ref Quaternion rotation, Vector3 targetPosition, Quaternion targetRotation);

		internal static TransformWithSnappingDelegate translateWithSnapping { get; set; }
		internal static DirectTransformWithSnappingDelegate directTransformWithSnapping { get; set; }
		internal static Action<Transform> clearSnappingState { get; set; }

		/// <summary>
		/// Translate a position vector using deltas while also respecting snapping
		/// </summary>
		/// <param name="rayOrigin">The ray doing the translating</param>
		/// <param name="objects">The objects being translated (used to determine bounds; Transforms do not get modified)</param>
		/// <param name="position">The position being modified by delta. This will be set with a snapped position if possible</param>
		/// <param name="rotation">The rotation to be modified if rotation snapping is enabled</param>
		/// <param name="delta">The position delta to apply</param>
		/// <returns>Whether the position was set to a snapped position</returns>
		public static bool TranslateWithSnapping(this IUsesSnapping usesSnaping, Transform rayOrigin, GameObject[] objects, ref Vector3 position, ref Quaternion rotation, Vector3 delta)
		{
			return translateWithSnapping(rayOrigin, objects, ref position, ref rotation, delta);
		}

		/// <summary>
		/// Transform a position/rotation directly while also respecting snapping
		/// </summary>
		/// <param name="rayOrigin">The ray doing the transforming</param>
		/// <param name="objects">The objects being transformed (used to determine bounds; Transforms do not get modified)</param>
		/// <param name="position">The position being transformed. This will be set to a snapped position if possible</param>
		/// <param name="rotation">The rotation being transformed. This will only be modified if rotation snapping is enabled</param>
		/// <param name="targetPosition">The input position provided by direct transformation</param>
		/// <param name="targetRotation">The input rotation provided by direct transformation</param>
		/// <returns></returns>
		public static bool DirectTransformWithSnapping(this IUsesSnapping usesSnaping, Transform rayOrigin, GameObject[] objects, ref Vector3 position, ref Quaternion rotation, Vector3 targetPosition, Quaternion targetRotation)
		{
			return directTransformWithSnapping(rayOrigin, objects, ref position, ref rotation, targetPosition, targetRotation);
		}

		/// <summary>
		/// Clear state information for a given ray
		/// </summary>
		/// <param name="rayOrigin">The ray whose state to clear</param>
		public static void ClearSnappingState(this IUsesSnapping usesSnaping, Transform rayOrigin)
		{
			clearSnappingState(rayOrigin);
		}
	}
}
