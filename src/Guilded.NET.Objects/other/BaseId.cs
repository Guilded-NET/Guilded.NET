namespace Guilded.NET.Objects {
    /// <summary>
    /// A base for all IDs.
    /// </summary>
    public class BaseId {
        /// <summary>
        /// Exception thrown when ID parsing fails.
        /// </summary>
        protected static readonly InvalidIdException IdParseException = new InvalidIdException("Could not parse the given ID string.");
        /// <summary>
        /// A base string for IDs.
        /// </summary>
        protected readonly string _;
        /// <summary>
        /// A base for all IDs.
        /// </summary>
        /// <param name="id">String which represents ID</param>
        protected BaseId(string id) =>
            _ = id;
        /// <summary>
        /// Converts ID to string.
        /// </summary>
        /// <returns>ID as string</returns>
        public override string ToString() => $"{_}";
        /// <summary>
        /// Gets ID hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => _.GetHashCode() * 2 - 1000;
        /// <summary>
        /// Checks if given object is equal to this ID.
        /// </summary>
        /// <param name="obj">Other object</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj) {
            if(obj is BaseId id) return id._ == _;
            else return false;
        }
        /// <summary>
        /// Checks if given ID is equal to this ID.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Boolean</returns>
        public static bool operator ==(BaseId id0, BaseId id1) => id0._ == id1._;
        /// <summary>
        /// Checks if given ID is not equal to this ID.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Boolean</returns>
        public static bool operator !=(BaseId id0, BaseId id1) => !(id0 == id1);
    }
}