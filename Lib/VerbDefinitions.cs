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
	public sealed class Verbs
	{
		public static Verb Abandoned
		{
			get
			{
				if (Verbs.abandoned == null)
				{
					Verbs.abandoned = new Verb("abandoned", "https://w3id.org/xapi/adl/verbs/abandoned");

					Verbs.abandoned.AddDisplayPair("en-US", "abandoned");
				}

				return Verbs.abandoned;
			}            
		}


		public static Verb Answered
		{
			get
			{
				if (Verbs.answered == null)
				{
					Verbs.answered = new Verb("answered", "https://adlnet.gov/expapi/verbs/answered");

					Verbs.answered.AddDisplayPair("de-DE", "beantwortete");
					Verbs.answered.AddDisplayPair("en-US", "answered");
					Verbs.answered.AddDisplayPair("fr-FR", "a répondu");
					Verbs.answered.AddDisplayPair("es-ES", "contestó");
				}

				return Verbs.answered;
			}            
		}


		public static Verb Asked
		{
			get
			{
				if (Verbs.asked == null)
				{
					Verbs.asked = new Verb("asked", "https://adlnet.gov/expapi/verbs/asked");

					Verbs.asked.AddDisplayPair("de-DE", "fragte");
					Verbs.asked.AddDisplayPair("en-US", "asked");
					Verbs.asked.AddDisplayPair("fr-FR", "a demandé");
					Verbs.asked.AddDisplayPair("es-ES", "preguntó");
				}

				return Verbs.asked;
			}            
		}


		public static Verb Attempted
		{
			get
			{
				if (Verbs.attempted == null)
				{
					Verbs.attempted = new Verb("attempted", "https://adlnet.gov/expapi/verbs/attempted");

					Verbs.attempted.AddDisplayPair("de-DE", "versuchte");
					Verbs.attempted.AddDisplayPair("en-US", "attempted");
					Verbs.attempted.AddDisplayPair("fr-FR", "a essayé");
					Verbs.attempted.AddDisplayPair("es-ES", "intentó");
				}

				return Verbs.attempted;
			}            
		}


		public static Verb Attended
		{
			get
			{
				if (Verbs.attended == null)
				{
					Verbs.attended = new Verb("attended", "https://adlnet.gov/expapi/verbs/attended");

					Verbs.attended.AddDisplayPair("de-DE", "nahm teil an");
					Verbs.attended.AddDisplayPair("en-US", "attended");
					Verbs.attended.AddDisplayPair("fr-FR", "a suivi");
					Verbs.attended.AddDisplayPair("es-ES", "asistió");
				}

				return Verbs.attended;
			}            
		}


		public static Verb Commented
		{
			get
			{
				if (Verbs.commented == null)
				{
					Verbs.commented = new Verb("commented", "https://adlnet.gov/expapi/verbs/commented");

					Verbs.commented.AddDisplayPair("de-DE", "kommentierte");
					Verbs.commented.AddDisplayPair("en-US", "commented");
					Verbs.commented.AddDisplayPair("fr-FR", "a commenté");
					Verbs.commented.AddDisplayPair("es-ES", "comentó");
				}

				return Verbs.commented;
			}            
		}


		public static Verb Completed
		{
			get
			{
				if (Verbs.completed == null)
				{
					Verbs.completed = new Verb("completed", "https://adlnet.gov/expapi/verbs/completed");

					Verbs.completed.AddDisplayPair("de-DE", "beendete");
					Verbs.completed.AddDisplayPair("en-US", "completed");
					Verbs.completed.AddDisplayPair("fr-FR", "a terminé");
					Verbs.completed.AddDisplayPair("es-ES", "completó");
				}

				return Verbs.completed;
			}            
		}


		public static Verb Exited
		{
			get
			{
				if (Verbs.exited == null)
				{
					Verbs.exited = new Verb("exited", "https://adlnet.gov/expapi/verbs/exited");

					Verbs.exited.AddDisplayPair("de-DE", "verließ");
					Verbs.exited.AddDisplayPair("en-US", "exited");
					Verbs.exited.AddDisplayPair("fr-FR", "a quitté");
					Verbs.exited.AddDisplayPair("es-ES", "salió");
				}

				return Verbs.exited;
			}            
		}


		public static Verb Experienced
		{
			get
			{
				if (Verbs.experienced == null)
				{
					Verbs.experienced = new Verb("experienced", "https://adlnet.gov/expapi/verbs/experienced");

					Verbs.experienced.AddDisplayPair("de-DE", "erlebte");
					Verbs.experienced.AddDisplayPair("en-US", "experienced");
					Verbs.experienced.AddDisplayPair("fr-FR", "a éprouvé");
					Verbs.experienced.AddDisplayPair("es-ES", "experimentó");
				}

				return Verbs.experienced;
			}            
		}


		public static Verb Failed
		{
			get
			{
				if (Verbs.failed == null)
				{
					Verbs.failed = new Verb("failed", "https://adlnet.gov/expapi/verbs/failed");

					Verbs.failed.AddDisplayPair("de-DE", "verfehlte");
					Verbs.failed.AddDisplayPair("en-US", "failed");
					Verbs.failed.AddDisplayPair("fr-FR", "a échoué");
					Verbs.failed.AddDisplayPair("es-ES", "fracasó");
				}

				return Verbs.failed;
			}            
		}


		public static Verb Imported
		{
			get
			{
				if (Verbs.imported == null)
				{
					Verbs.imported = new Verb("imported", "https://adlnet.gov/expapi/verbs/imported");

					Verbs.imported.AddDisplayPair("de-DE", "importierte");
					Verbs.imported.AddDisplayPair("en-US", "imported");
					Verbs.imported.AddDisplayPair("fr-FR", "a importé");
					Verbs.imported.AddDisplayPair("es-ES", "importó");
				}

				return Verbs.imported;
			}            
		}


		public static Verb Initialized
		{
			get
			{
				if (Verbs.initialized == null)
				{
					Verbs.initialized = new Verb("initialized", "https://adlnet.gov/expapi/verbs/initialized");

					Verbs.initialized.AddDisplayPair("de-DE", "initialisierte");
					Verbs.initialized.AddDisplayPair("en-US", "initialized");
					Verbs.initialized.AddDisplayPair("fr-FR", "a initialisé");
					Verbs.initialized.AddDisplayPair("es-ES", "inicializó");
				}

				return Verbs.initialized;
			}            
		}


		public static Verb Interacted
		{
			get
			{
				if (Verbs.interacted == null)
				{
					Verbs.interacted = new Verb("interacted", "https://adlnet.gov/expapi/verbs/interacted");

					Verbs.interacted.AddDisplayPair("de-DE", "interagierte");
					Verbs.interacted.AddDisplayPair("en-US", "interacted");
					Verbs.interacted.AddDisplayPair("fr-FR", "a interagi");
					Verbs.interacted.AddDisplayPair("es-ES", "interactuó");
				}

				return Verbs.interacted;
			}            
		}


		public static Verb Launched
		{
			get
			{
				if (Verbs.launched == null)
				{
					Verbs.launched = new Verb("launched", "https://adlnet.gov/expapi/verbs/launched");

					Verbs.launched.AddDisplayPair("de-DE", "startete");
					Verbs.launched.AddDisplayPair("en-US", "launched");
					Verbs.launched.AddDisplayPair("fr-FR", "a lancé");
					Verbs.launched.AddDisplayPair("es-ES", "lanzó");
				}

				return Verbs.launched;
			}            
		}


		public static Verb Mastered
		{
			get
			{
				if (Verbs.mastered == null)
				{
					Verbs.mastered = new Verb("mastered", "https://adlnet.gov/expapi/verbs/mastered");

					Verbs.mastered.AddDisplayPair("de-DE", "meisterte");
					Verbs.mastered.AddDisplayPair("en-US", "mastered");
					Verbs.mastered.AddDisplayPair("fr-FR", "a maîtrisé");
					Verbs.mastered.AddDisplayPair("es-ES", "dominó");
				}

				return Verbs.mastered;
			}            
		}


		public static Verb Passed
		{
			get
			{
				if (Verbs.passed == null)
				{
					Verbs.passed = new Verb("passed", "https://adlnet.gov/expapi/verbs/passed");

					Verbs.passed.AddDisplayPair("de-DE", "bestand");
					Verbs.passed.AddDisplayPair("en-US", "passed");
					Verbs.passed.AddDisplayPair("fr-FR", "a réussi");
					Verbs.passed.AddDisplayPair("es-ES", "aprobó");
				}

				return Verbs.passed;
			}            
		}


		public static Verb Preferred
		{
			get
			{
				if (Verbs.preferred == null)
				{
					Verbs.preferred = new Verb("preferred", "https://adlnet.gov/expapi/verbs/preferred");

					Verbs.preferred.AddDisplayPair("de-DE", "bevorzugte");
					Verbs.preferred.AddDisplayPair("en-US", "preferred");
					Verbs.preferred.AddDisplayPair("fr-FR", "a préféré");
					Verbs.preferred.AddDisplayPair("es-ES", "prefirió");
				}

				return Verbs.preferred;
			}            
		}


		public static Verb Progressed
		{
			get
			{
				if (Verbs.progressed == null)
				{
					Verbs.progressed = new Verb("progressed", "https://adlnet.gov/expapi/verbs/progressed");

					Verbs.progressed.AddDisplayPair("de-DE", "machte Fortschritt mit");
					Verbs.progressed.AddDisplayPair("en-US", "progressed");
					Verbs.progressed.AddDisplayPair("fr-FR", "a progressé");
					Verbs.progressed.AddDisplayPair("es-ES", "progresó");
				}

				return Verbs.progressed;
			}            
		}


		public static Verb Registered
		{
			get
			{
				if (Verbs.registered == null)
				{
					Verbs.registered = new Verb("registered", "https://adlnet.gov/expapi/verbs/registered");

					Verbs.registered.AddDisplayPair("de-DE", "registrierte");
					Verbs.registered.AddDisplayPair("en-US", "registered");
					Verbs.registered.AddDisplayPair("fr-FR", "a enregistré");
					Verbs.registered.AddDisplayPair("es-ES", "registró");
				}

				return Verbs.registered;
			}            
		}


		public static Verb Responded
		{
			get
			{
				if (Verbs.responded == null)
				{
					Verbs.responded = new Verb("responded", "https://adlnet.gov/expapi/verbs/responded");

					Verbs.responded.AddDisplayPair("de-DE", "reagierte");
					Verbs.responded.AddDisplayPair("en-US", "responded");
					Verbs.responded.AddDisplayPair("fr-FR", "a répondu");
					Verbs.responded.AddDisplayPair("es-ES", "respondió");
				}

				return Verbs.responded;
			}            
		}


		public static Verb Resumed
		{
			get
			{
				if (Verbs.resumed == null)
				{
					Verbs.resumed = new Verb("resumed", "https://adlnet.gov/expapi/verbs/resumed");

					Verbs.resumed.AddDisplayPair("de-DE", "setzte fort");
					Verbs.resumed.AddDisplayPair("en-US", "resumed");
					Verbs.resumed.AddDisplayPair("fr-FR", "a repris");
					Verbs.resumed.AddDisplayPair("es-ES", "continuó");
				}

				return Verbs.resumed;
			}            
		}


		public static Verb Satisfied
		{
			get
			{
				if (Verbs.satisfied == null)
				{
					Verbs.satisfied = new Verb("satisfied", "https://w3id.org/xapi/adl/verbs/satisfied");

					Verbs.satisfied.AddDisplayPair("en-US", "satisfied");
				}

				return Verbs.satisfied;
			}            
		}


		public static Verb Scored
		{
			get
			{
				if (Verbs.scored == null)
				{
					Verbs.scored = new Verb("scored", "https://adlnet.gov/expapi/verbs/scored");

					Verbs.scored.AddDisplayPair("de-DE", "erreichte");
					Verbs.scored.AddDisplayPair("en-US", "scored");
					Verbs.scored.AddDisplayPair("fr-FR", "a marqué");
					Verbs.scored.AddDisplayPair("es-ES", "anotó");
				}

				return Verbs.scored;
			}            
		}


		public static Verb Shared
		{
			get
			{
				if (Verbs.shared == null)
				{
					Verbs.shared = new Verb("shared", "https://adlnet.gov/expapi/verbs/shared");

					Verbs.shared.AddDisplayPair("de-DE", "teilte");
					Verbs.shared.AddDisplayPair("en-US", "shared");
					Verbs.shared.AddDisplayPair("fr-FR", "a partagé");
					Verbs.shared.AddDisplayPair("es-ES", "compartió");
				}

				return Verbs.shared;
			}            
		}


		public static Verb Suspended
		{
			get
			{
				if (Verbs.suspended == null)
				{
					Verbs.suspended = new Verb("suspended", "https://adlnet.gov/expapi/verbs/suspended");

					Verbs.suspended.AddDisplayPair("de-DE", "pausierte");
					Verbs.suspended.AddDisplayPair("en-US", "suspended");
					Verbs.suspended.AddDisplayPair("fr-FR", "a suspendu");
					Verbs.suspended.AddDisplayPair("es-ES", "aplazó");
				}

				return Verbs.suspended;
			}            
		}


		public static Verb Terminated
		{
			get
			{
				if (Verbs.terminated == null)
				{
					Verbs.terminated = new Verb("terminated", "https://adlnet.gov/expapi/verbs/terminated");

					Verbs.terminated.AddDisplayPair("de-DE", "beendete");
					Verbs.terminated.AddDisplayPair("en-US", "terminated");
					Verbs.terminated.AddDisplayPair("fr-FR", "a terminé");
					Verbs.terminated.AddDisplayPair("es-ES", "terminó");
				}

				return Verbs.terminated;
			}            
		}


		public static Verb Waived
		{
			get
			{
				if (Verbs.waived == null)
				{
					Verbs.waived = new Verb("waived", "https://w3id.org/xapi/adl/verbs/waived");

					Verbs.waived.AddDisplayPair("en-US", "waived");
				}

				return Verbs.waived;
			}            
		}

		// Private Fields
		private static Verb abandoned;
		private static Verb answered;
		private static Verb asked;
		private static Verb attempted;
		private static Verb attended;
		private static Verb commented;
		private static Verb completed;
		private static Verb exited;
		private static Verb experienced;
		private static Verb failed;
		private static Verb imported;
		private static Verb initialized;
		private static Verb interacted;
		private static Verb launched;
		private static Verb mastered;
		private static Verb passed;
		private static Verb preferred;
		private static Verb progressed;
		private static Verb registered;
		private static Verb responded;
		private static Verb resumed;
		private static Verb satisfied;
		private static Verb scored;
		private static Verb shared;
		private static Verb suspended;
		private static Verb terminated;
		private static Verb voided;
		private static Verb waived;
	}

	/// <summary>
	/// Collection of Special verbs with unique properties when used in a statement.
	/// </summary>
	public static class VerbsSpecial
	{
		/// <summary>
		/// Gets the verb "Voided".
		/// 
		/// This is a special verb that MUST be used to Void another statement. That other statement
		/// MUST be the object of the statement whose Verb is Voided, using the StatementRef Object Type.
		/// </summary>
		/// <value>The voided.</value>
		public static Verb Voided
		{
			get
			{
				if (VerbsSpecial.voided == null)
				{
					VerbsSpecial.voided = new Verb("voided", "https://adlnet.gov/expapi/verbs/voided");

					VerbsSpecial.voided.AddDisplayPair("de-DE", "entwertete");
					VerbsSpecial.voided.AddDisplayPair("en-US", "voided");
					VerbsSpecial.voided.AddDisplayPair("fr-FR", "a annulé");
					VerbsSpecial.voided.AddDisplayPair("es-ES", "anuló");
				}

				return VerbsSpecial.voided;
			}            
		}

		private static Verb voided;
	}
}