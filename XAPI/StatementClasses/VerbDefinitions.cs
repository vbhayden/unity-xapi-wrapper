using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XAPI
{
	/// <summary>
	/// Helper class with properties to access premade Verbs.
	/// 
	/// This class / file were built by parsing the JS XAPIWrapper's Verb definitions from GitHub:
	/// https://github.com/adlnet/xAPIWrapper/blob/master/src/verbs.js
	/// </summary>
	public sealed class XVerbs
	{
		public static XVerb Abandoned
		{
			get
			{
				if (XVerbs.abandoned == null)
				{
					XVerbs.abandoned = new XVerb("abandoned", "https://w3id.org/xapi/adl/verbs/abandoned");

					XVerbs.abandoned.AddDisplayPair("en-US", "abandoned");
				}

				return XVerbs.abandoned;
			}            
		}


		public static XVerb Answered
		{
			get
			{
				if (XVerbs.answered == null)
				{
					XVerbs.answered = new XVerb("answered", "http://adlnet.gov/expapi/verbs/answered");

					XVerbs.answered.AddDisplayPair("de-DE", "beantwortete");
					XVerbs.answered.AddDisplayPair("en-US", "answered");
					XVerbs.answered.AddDisplayPair("fr-FR", "a répondu");
					XVerbs.answered.AddDisplayPair("es-ES", "contestó");
				}

				return XVerbs.answered;
			}            
		}


		public static XVerb Asked
		{
			get
			{
				if (XVerbs.asked == null)
				{
					XVerbs.asked = new XVerb("asked", "http://adlnet.gov/expapi/verbs/asked");

					XVerbs.asked.AddDisplayPair("de-DE", "fragte");
					XVerbs.asked.AddDisplayPair("en-US", "asked");
					XVerbs.asked.AddDisplayPair("fr-FR", "a demandé");
					XVerbs.asked.AddDisplayPair("es-ES", "preguntó");
				}

				return XVerbs.asked;
			}            
		}


		public static XVerb Attempted
		{
			get
			{
				if (XVerbs.attempted == null)
				{
					XVerbs.attempted = new XVerb("attempted", "http://adlnet.gov/expapi/verbs/attempted");

					XVerbs.attempted.AddDisplayPair("de-DE", "versuchte");
					XVerbs.attempted.AddDisplayPair("en-US", "attempted");
					XVerbs.attempted.AddDisplayPair("fr-FR", "a essayé");
					XVerbs.attempted.AddDisplayPair("es-ES", "intentó");
				}

				return XVerbs.attempted;
			}            
		}


		public static XVerb Attended
		{
			get
			{
				if (XVerbs.attended == null)
				{
					XVerbs.attended = new XVerb("attended", "http://adlnet.gov/expapi/verbs/attended");

					XVerbs.attended.AddDisplayPair("de-DE", "nahm teil an");
					XVerbs.attended.AddDisplayPair("en-US", "attended");
					XVerbs.attended.AddDisplayPair("fr-FR", "a suivi");
					XVerbs.attended.AddDisplayPair("es-ES", "asistió");
				}

				return XVerbs.attended;
			}            
		}


		public static XVerb Commented
		{
			get
			{
				if (XVerbs.commented == null)
				{
					XVerbs.commented = new XVerb("commented", "http://adlnet.gov/expapi/verbs/commented");

					XVerbs.commented.AddDisplayPair("de-DE", "kommentierte");
					XVerbs.commented.AddDisplayPair("en-US", "commented");
					XVerbs.commented.AddDisplayPair("fr-FR", "a commenté");
					XVerbs.commented.AddDisplayPair("es-ES", "comentó");
				}

				return XVerbs.commented;
			}            
		}


		public static XVerb Completed
		{
			get
			{
				if (XVerbs.completed == null)
				{
					XVerbs.completed = new XVerb("completed", "http://adlnet.gov/expapi/verbs/completed");

					XVerbs.completed.AddDisplayPair("de-DE", "beendete");
					XVerbs.completed.AddDisplayPair("en-US", "completed");
					XVerbs.completed.AddDisplayPair("fr-FR", "a terminé");
					XVerbs.completed.AddDisplayPair("es-ES", "completó");
				}

				return XVerbs.completed;
			}            
		}


		public static XVerb Exited
		{
			get
			{
				if (XVerbs.exited == null)
				{
					XVerbs.exited = new XVerb("exited", "http://adlnet.gov/expapi/verbs/exited");

					XVerbs.exited.AddDisplayPair("de-DE", "verließ");
					XVerbs.exited.AddDisplayPair("en-US", "exited");
					XVerbs.exited.AddDisplayPair("fr-FR", "a quitté");
					XVerbs.exited.AddDisplayPair("es-ES", "salió");
				}

				return XVerbs.exited;
			}            
		}


		public static XVerb Experienced
		{
			get
			{
				if (XVerbs.experienced == null)
				{
					XVerbs.experienced = new XVerb("experienced", "http://adlnet.gov/expapi/verbs/experienced");

					XVerbs.experienced.AddDisplayPair("de-DE", "erlebte");
					XVerbs.experienced.AddDisplayPair("en-US", "experienced");
					XVerbs.experienced.AddDisplayPair("fr-FR", "a éprouvé");
					XVerbs.experienced.AddDisplayPair("es-ES", "experimentó");
				}

				return XVerbs.experienced;
			}            
		}


		public static XVerb Failed
		{
			get
			{
				if (XVerbs.failed == null)
				{
					XVerbs.failed = new XVerb("failed", "http://adlnet.gov/expapi/verbs/failed");

					XVerbs.failed.AddDisplayPair("de-DE", "verfehlte");
					XVerbs.failed.AddDisplayPair("en-US", "failed");
					XVerbs.failed.AddDisplayPair("fr-FR", "a échoué");
					XVerbs.failed.AddDisplayPair("es-ES", "fracasó");
				}

				return XVerbs.failed;
			}            
		}


		public static XVerb Imported
		{
			get
			{
				if (XVerbs.imported == null)
				{
					XVerbs.imported = new XVerb("imported", "http://adlnet.gov/expapi/verbs/imported");

					XVerbs.imported.AddDisplayPair("de-DE", "importierte");
					XVerbs.imported.AddDisplayPair("en-US", "imported");
					XVerbs.imported.AddDisplayPair("fr-FR", "a importé");
					XVerbs.imported.AddDisplayPair("es-ES", "importó");
				}

				return XVerbs.imported;
			}            
		}


		public static XVerb Initialized
		{
			get
			{
				if (XVerbs.initialized == null)
				{
					XVerbs.initialized = new XVerb("initialized", "http://adlnet.gov/expapi/verbs/initialized");

					XVerbs.initialized.AddDisplayPair("de-DE", "initialisierte");
					XVerbs.initialized.AddDisplayPair("en-US", "initialized");
					XVerbs.initialized.AddDisplayPair("fr-FR", "a initialisé");
					XVerbs.initialized.AddDisplayPair("es-ES", "inicializó");
				}

				return XVerbs.initialized;
			}            
		}


		public static XVerb Interacted
		{
			get
			{
				if (XVerbs.interacted == null)
				{
					XVerbs.interacted = new XVerb("interacted", "http://adlnet.gov/expapi/verbs/interacted");

					XVerbs.interacted.AddDisplayPair("de-DE", "interagierte");
					XVerbs.interacted.AddDisplayPair("en-US", "interacted");
					XVerbs.interacted.AddDisplayPair("fr-FR", "a interagi");
					XVerbs.interacted.AddDisplayPair("es-ES", "interactuó");
				}

				return XVerbs.interacted;
			}            
		}


		public static XVerb Launched
		{
			get
			{
				if (XVerbs.launched == null)
				{
					XVerbs.launched = new XVerb("launched", "http://adlnet.gov/expapi/verbs/launched");

					XVerbs.launched.AddDisplayPair("de-DE", "startete");
					XVerbs.launched.AddDisplayPair("en-US", "launched");
					XVerbs.launched.AddDisplayPair("fr-FR", "a lancé");
					XVerbs.launched.AddDisplayPair("es-ES", "lanzó");
				}

				return XVerbs.launched;
			}            
		}


		public static XVerb Mastered
		{
			get
			{
				if (XVerbs.mastered == null)
				{
					XVerbs.mastered = new XVerb("mastered", "http://adlnet.gov/expapi/verbs/mastered");

					XVerbs.mastered.AddDisplayPair("de-DE", "meisterte");
					XVerbs.mastered.AddDisplayPair("en-US", "mastered");
					XVerbs.mastered.AddDisplayPair("fr-FR", "a maîtrisé");
					XVerbs.mastered.AddDisplayPair("es-ES", "dominó");
				}

				return XVerbs.mastered;
			}            
		}


		public static XVerb Passed
		{
			get
			{
				if (XVerbs.passed == null)
				{
					XVerbs.passed = new XVerb("passed", "http://adlnet.gov/expapi/verbs/passed");

					XVerbs.passed.AddDisplayPair("de-DE", "bestand");
					XVerbs.passed.AddDisplayPair("en-US", "passed");
					XVerbs.passed.AddDisplayPair("fr-FR", "a réussi");
					XVerbs.passed.AddDisplayPair("es-ES", "aprobó");
				}

				return XVerbs.passed;
			}            
		}


		public static XVerb Preferred
		{
			get
			{
				if (XVerbs.preferred == null)
				{
					XVerbs.preferred = new XVerb("preferred", "http://adlnet.gov/expapi/verbs/preferred");

					XVerbs.preferred.AddDisplayPair("de-DE", "bevorzugte");
					XVerbs.preferred.AddDisplayPair("en-US", "preferred");
					XVerbs.preferred.AddDisplayPair("fr-FR", "a préféré");
					XVerbs.preferred.AddDisplayPair("es-ES", "prefirió");
				}

				return XVerbs.preferred;
			}            
		}


		public static XVerb Progressed
		{
			get
			{
				if (XVerbs.progressed == null)
				{
					XVerbs.progressed = new XVerb("progressed", "http://adlnet.gov/expapi/verbs/progressed");

					XVerbs.progressed.AddDisplayPair("de-DE", "machte Fortschritt mit");
					XVerbs.progressed.AddDisplayPair("en-US", "progressed");
					XVerbs.progressed.AddDisplayPair("fr-FR", "a progressé");
					XVerbs.progressed.AddDisplayPair("es-ES", "progresó");
				}

				return XVerbs.progressed;
			}            
		}


		public static XVerb Registered
		{
			get
			{
				if (XVerbs.registered == null)
				{
					XVerbs.registered = new XVerb("registered", "http://adlnet.gov/expapi/verbs/registered");

					XVerbs.registered.AddDisplayPair("de-DE", "registrierte");
					XVerbs.registered.AddDisplayPair("en-US", "registered");
					XVerbs.registered.AddDisplayPair("fr-FR", "a enregistré");
					XVerbs.registered.AddDisplayPair("es-ES", "registró");
				}

				return XVerbs.registered;
			}            
		}


		public static XVerb Responded
		{
			get
			{
				if (XVerbs.responded == null)
				{
					XVerbs.responded = new XVerb("responded", "http://adlnet.gov/expapi/verbs/responded");

					XVerbs.responded.AddDisplayPair("de-DE", "reagierte");
					XVerbs.responded.AddDisplayPair("en-US", "responded");
					XVerbs.responded.AddDisplayPair("fr-FR", "a répondu");
					XVerbs.responded.AddDisplayPair("es-ES", "respondió");
				}

				return XVerbs.responded;
			}            
		}


		public static XVerb Resumed
		{
			get
			{
				if (XVerbs.resumed == null)
				{
					XVerbs.resumed = new XVerb("resumed", "http://adlnet.gov/expapi/verbs/resumed");

					XVerbs.resumed.AddDisplayPair("de-DE", "setzte fort");
					XVerbs.resumed.AddDisplayPair("en-US", "resumed");
					XVerbs.resumed.AddDisplayPair("fr-FR", "a repris");
					XVerbs.resumed.AddDisplayPair("es-ES", "continuó");
				}

				return XVerbs.resumed;
			}            
		}


		public static XVerb Satisfied
		{
			get
			{
				if (XVerbs.satisfied == null)
				{
					XVerbs.satisfied = new XVerb("satisfied", "https://w3id.org/xapi/adl/verbs/satisfied");

					XVerbs.satisfied.AddDisplayPair("en-US", "satisfied");
				}

				return XVerbs.satisfied;
			}            
		}


		public static XVerb Scored
		{
			get
			{
				if (XVerbs.scored == null)
				{
					XVerbs.scored = new XVerb("scored", "http://adlnet.gov/expapi/verbs/scored");

					XVerbs.scored.AddDisplayPair("de-DE", "erreichte");
					XVerbs.scored.AddDisplayPair("en-US", "scored");
					XVerbs.scored.AddDisplayPair("fr-FR", "a marqué");
					XVerbs.scored.AddDisplayPair("es-ES", "anotó");
				}

				return XVerbs.scored;
			}            
		}


		public static XVerb Shared
		{
			get
			{
				if (XVerbs.shared == null)
				{
					XVerbs.shared = new XVerb("shared", "http://adlnet.gov/expapi/verbs/shared");

					XVerbs.shared.AddDisplayPair("de-DE", "teilte");
					XVerbs.shared.AddDisplayPair("en-US", "shared");
					XVerbs.shared.AddDisplayPair("fr-FR", "a partagé");
					XVerbs.shared.AddDisplayPair("es-ES", "compartió");
				}

				return XVerbs.shared;
			}            
		}


		public static XVerb Suspended
		{
			get
			{
				if (XVerbs.suspended == null)
				{
					XVerbs.suspended = new XVerb("suspended", "http://adlnet.gov/expapi/verbs/suspended");

					XVerbs.suspended.AddDisplayPair("de-DE", "pausierte");
					XVerbs.suspended.AddDisplayPair("en-US", "suspended");
					XVerbs.suspended.AddDisplayPair("fr-FR", "a suspendu");
					XVerbs.suspended.AddDisplayPair("es-ES", "aplazó");
				}

				return XVerbs.suspended;
			}            
		}


		public static XVerb Terminated
		{
			get
			{
				if (XVerbs.terminated == null)
				{
					XVerbs.terminated = new XVerb("terminated", "http://adlnet.gov/expapi/verbs/terminated");

					XVerbs.terminated.AddDisplayPair("de-DE", "beendete");
					XVerbs.terminated.AddDisplayPair("en-US", "terminated");
					XVerbs.terminated.AddDisplayPair("fr-FR", "a terminé");
					XVerbs.terminated.AddDisplayPair("es-ES", "terminó");
				}

				return XVerbs.terminated;
			}            
		}


		public static XVerb Waived
		{
			get
			{
				if (XVerbs.waived == null)
				{
					XVerbs.waived = new XVerb("waived", "https://w3id.org/xapi/adl/verbs/waived");

					XVerbs.waived.AddDisplayPair("en-US", "waived");
				}

				return XVerbs.waived;
			}            
		}

		// Private Fields
		private static XVerb abandoned;
		private static XVerb answered;
		private static XVerb asked;
		private static XVerb attempted;
		private static XVerb attended;
		private static XVerb commented;
		private static XVerb completed;
		private static XVerb exited;
		private static XVerb experienced;
		private static XVerb failed;
		private static XVerb imported;
		private static XVerb initialized;
		private static XVerb interacted;
		private static XVerb launched;
		private static XVerb mastered;
		private static XVerb passed;
		private static XVerb preferred;
		private static XVerb progressed;
		private static XVerb registered;
		private static XVerb responded;
		private static XVerb resumed;
		private static XVerb satisfied;
		private static XVerb scored;
		private static XVerb shared;
		private static XVerb suspended;
		private static XVerb terminated;
		private static XVerb voided;
		private static XVerb waived;
	}

	/// <summary>
	/// Collection of Special verbs with unique properties when used in a statement.
	/// </summary>
	public static class XVerbsSpecial
	{
		/// <summary>
		/// Gets the verb "Voided".
		/// 
		/// This is a special verb that MUST be used to Void another statement. That other statement
		/// MUST be the object of the statement whose Verb is Voided, using the StatementRef Object Type.
		/// </summary>
		/// <value>The voided.</value>
		public static XVerb Voided
		{
			get
			{
				if (XVerbsSpecial.voided == null)
				{
					XVerbsSpecial.voided = new XVerb("voided", "http://adlnet.gov/expapi/verbs/voided");

					XVerbsSpecial.voided.AddDisplayPair("de-DE", "entwertete");
					XVerbsSpecial.voided.AddDisplayPair("en-US", "voided");
					XVerbsSpecial.voided.AddDisplayPair("fr-FR", "a annulé");
					XVerbsSpecial.voided.AddDisplayPair("es-ES", "anuló");
				}

				return XVerbsSpecial.voided;
			}            
		}

		private static XVerb voided;
	}
}