using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UDEngine;

using DG.Tweening;

namespace UDEngine.Plugin.ShootDust {
	public static class USimpleFadeDust {
		public static USimpleFadeDustObject Create(Transform transParent, Sprite sprite, bool useParentTransSettings = true, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion)) {
			return new USimpleFadeDustObject (transParent, sprite, useParentTransSettings, position, rotation);
		}
	}

	public class USimpleFadeDustObject {
		public USimpleFadeDustObject(Transform transParent, Sprite sprite, bool useParentTransSettings = true, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion)) {
			trans = new GameObject ("FadeDust").transform;

			if (useParentTransSettings) {
				trans.position = transParent.position;
				trans.rotation = transParent.rotation;
			} else {
				trans.position = position;
				trans.rotation = rotation;
			}

			trans.parent = transParent;

			this.spriteRenderer = trans.gameObject.AddComponent<SpriteRenderer> ();
			this.spriteRenderer.sprite = sprite;

		}

		public Transform trans;
		public SpriteRenderer spriteRenderer;

		public Tweener fadeTween;

		public USimpleFadeDustObject Fade(float time) {
			this.Reset ();

			fadeTween = this.spriteRenderer.DOFade (0f, time);

			return this;
		}

		public USimpleFadeDustObject Reset() {
			if (fadeTween != null) {
				fadeTween.Kill ();
			}

			Color lastColor = spriteRenderer.color;
			lastColor.a = 1f;
			spriteRenderer.color = lastColor;

			return this;
		}

		public void Destroy() {
			if (fadeTween != null) {
				fadeTween.Kill ();
				fadeTween = null;
			}

			MonoBehaviour.Destroy (trans.gameObject);

			spriteRenderer = null;
			trans = null;
		}
	}
}
