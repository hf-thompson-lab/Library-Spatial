// Copyright 2005-2006 University of Wisconsin
// All rights reserved. 
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Landis.SpatialModeling;

namespace Landis.Landscapes
{
    /// <summary>
    /// Base class for an enumerator for a collection of sites or their
    /// locations.
    /// </summary>
    public abstract class EnumeratorBase
        : System.IDisposable
    {
        /// <summary>
        /// Various states that an enumerator can be in.
        /// </summary>
        public enum State
        {
            BeforeFirst,
            InCollection,
            AfterLast
        }

        //---------------------------------------------------------------------

        private State state;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected EnumeratorBase()
        {
            state = State.BeforeFirst;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The current state of the enumerator.
        /// </summary>
        protected State CurrentState
        {
            get {
                return state;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Updates the enumerator's state for a call to its MoveNext method
        /// that was successful.
        /// </summary>
        /// <returns>
        /// true, so the MoveNext method in the derived class can call this
        /// method like this:
        /// <pre>
        ///     return MoveNextSucceeded();
        /// </pre>
        /// </returns>
        protected bool MoveNextSucceeded()
        {
            state = State.InCollection;
            return true;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Updates the enumerator's state for a call to its MoveNext method
        /// that reached the end of the collection.
        /// </summary>
        /// <returns>
        /// false, so the MoveNext method in the derived class can call this
        /// method like this:
        /// <pre>
        ///     return MoveNextReachedEnd();
        /// </pre>
        /// </returns>
        protected bool MoveNextReachedEnd()
        {
            state = State.AfterLast;
            return false;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Resets the enumerator's state.
        /// </summary>
        public virtual void Reset()
        {
            state = State.BeforeFirst;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Disposes of the object, releasing all unmanaged resources that it
        /// has.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
